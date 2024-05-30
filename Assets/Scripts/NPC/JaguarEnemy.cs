using System.Collections;
using System.Collections.Generic;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using UnityEngine;
using UnityEngine.Serialization;

public class JaguarEnemy : HP
{
    [Header("Enemy Settings")]
    public LayerMask belowLayer;
    public LayerMask frontLayer;
    public LayerMask attackLayer;
    public LayerMask detectionEar;
    public float belowDistance;
    public float frontDistance;
    public float backDistance;
    public float frontAttackDistance;
    public Transform belowController;
    public Transform frontController;
    public Transform backController;
    public Transform AttackController;
    public bool belowInfoJaguar;
    public bool frontInfoJaguar;
    public bool backInfoJaguar;
    public bool attackinfoJaguar;
    private float runspeed;

    private bool LookToRight = false;
    

    [Header("attack Setings")]
    [SerializeField] private Transform AttackOperator;
    [SerializeField] private float AttackRadio;
    [SerializeField] private float AttackDamage;
    [SerializeField] private float TimeBetweenAttack;
    [SerializeField] private float TimeNextAttack;
    [SerializeField] private float AttackDuration;
    [SerializeField] private float KBHitForece;
    private string str;
    [Header("Sound Settings")]
    [SerializeField] private AudioClip AttackAudioClip;
    [SerializeField] private AudioSource audioSource;
    private bool canSound;

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        runspeed=HorizontalMovement;
        
        if(!TrainingMode) MaxStep = 0;
    }

    public override void OnEpisodeBegin()
    {
        runspeed = HorizontalMovement;
        CurrentHealth = MaxHealth;
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        frontInfoJaguar = Physics2D.Raycast(frontController.position, transform.right, frontDistance, frontLayer);
        attackinfoJaguar = Physics2D.Raycast(frontController.position, transform.right, frontDistance, attackLayer);
        belowInfoJaguar = Physics2D.Raycast(belowController.position, transform.up * -1, belowDistance, belowLayer);
        bool inFloorJaguar = inFloor;
        float invincibleTimeJaguar = invincibleTime;
        float CurrentHealthJaguar = CurrentHealth;
        sensor.AddObservation(frontInfoJaguar);
        sensor.AddObservation(attackinfoJaguar);
        sensor.AddObservation(belowInfoJaguar);
        sensor.AddObservation(inFloorJaguar);
        sensor.AddObservation(invincibleTimeJaguar);
        sensor.AddObservation(CurrentHealthJaguar);
        
    }
    public override void OnActionReceived(ActionBuffers actions)
    {
        base.OnActionReceived(actions);

        // Supongamos que las acciones contienen tres valores: el movimiento horizontal, un indicador de ataque y un indicador de salto
        runspeed = actions.ContinuousActions[0];
        bool attack = actions.DiscreteActions[0] == 1;
        bool turn = actions.DiscreteActions[1] == 1;
        jump = actions.DiscreteActions[2] == 1;
        animator.SetInteger("Walk",(int)HorizontalMovement);

        if(attack && TimeNextAttack <=0 && inFloor){
            animator.SetTrigger("AttackTrigger");
            Invoke("CharacterHit",AttackDuration);
            TimeNextAttack=TimeBetweenAttack;
            canSound=true;
        }

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("run")){
            canMove=false;
            float FixedSpeed;
            str="run";
            StartCoroutine(animWait());
            if(LD){
                FixedSpeed= runspeed*1*.1f;
            }else{
                FixedSpeed= runspeed*-1*.1f;
            }
            rb.velocity=new Vector2(FixedSpeed,rb.velocityY);
        }

        if(turn)
        {
            //Girar
            Girar();
        }
        
    }
    public override void Heuristic(in ActionBuffers actionsOut)
    {
        if(invincibleTime>0){
            invincibleTime -= Time.deltaTime;
        }
        //Move(HorizontalMovement,false);
        frontInfoJaguar = Physics2D.Raycast(frontController.position, transform.right, frontDistance, frontLayer);
        attackinfoJaguar = Physics2D.Raycast(AttackController.position, transform.right, frontAttackDistance, attackLayer);
        belowInfoJaguar = Physics2D.Raycast(belowController.position, transform.up * -1, belowDistance, belowLayer);
        backInfoJaguar = Physics2D.Raycast(backController.position, transform.right, backDistance, detectionEar);
        animator.SetInteger("Walk",(int)HorizontalMovement);
        if (Input.GetKeyDown(KeyCode.I))
            {
                jump = true;
                animator.SetBool("Jump",true);
            }
        if(TimeNextAttack>0){
            TimeNextAttack -= Time.deltaTime;
        }
        if(attackinfoJaguar && TimeNextAttack <=0 && inFloor){
            animator.SetTrigger("AttackTrigger");
            Invoke("CharacterHit",AttackDuration);
            TimeNextAttack=TimeBetweenAttack;
            canSound=true;
        }
        if(animator.GetCurrentAnimatorStateInfo(0).IsName("attack")){
            canMove=false;
            str="attack";
            StartCoroutine(animWait());
            if(canSound){
                StartCoroutine(soundWait());
            }
            float FixedSpeed;
            if(LD){
                FixedSpeed= runspeed*1*.035f;
            }else{
                FixedSpeed= runspeed*-1*.035f;
            }
            rb.velocity=new Vector2(FixedSpeed,rb.velocityY);
            
        }
        
        if(frontInfoJaguar || !belowInfoJaguar || backInfoJaguar)
        {
            //Girar
            Girar();
        }
        if(!inFloor){
            if(rb.velocityY>0){
                animator.SetBool("Jump",true);
                animator.SetBool("Fall",false);
            }else{
                animator.SetBool("Jump",false);
                animator.SetBool("Fall",true);
            }

        }else{
                animator.SetBool("Fall",false);  
                animator.SetBool("Jump",false);
        }
        if(!Live){
            canMove=false;
            rb.velocity=new Vector2(0,0);
            animator.SetTrigger("DIE");
        }
        
    }

    private void Girar()
    {
        LookToRight = !LookToRight;
        //transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        HorizontalMovement *= -1;
    }
    private void CharacterHit(){
        Collider2D[] Objects = Physics2D.OverlapCircleAll(AttackOperator.position, AttackRadio);
        foreach (Collider2D colition in Objects){
            if(colition.CompareTag("Player")){
                colition.transform.GetComponent<HP>().Damage(AttackDamage,transform,KBHitForece);
                colition.transform.GetComponent<hpSystem>().hpBarChange();                
            }
        }
    }
    private IEnumerator animWait(){
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName(str));
        canMove=true;
    }
    private IEnumerator DeathAnimWait(){
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("DED"));
        
    }
    private IEnumerator soundWait(){
        yield return new WaitForSeconds(1);
        if(!audioSource.isPlaying&&canSound){
            canSound=false;
            audioSource.PlayOneShot(AttackAudioClip);
        }
    }
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<HP>().Damage(1,transform,5);
            other.transform.GetComponent<hpSystem>().hpBarChange();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(belowController.transform.position, belowController.transform.position + transform.up * -1 *  belowDistance);
        Gizmos.DrawLine(frontController.transform.position, frontController.transform.position + transform.right * frontDistance);
        Gizmos.DrawLine(AttackController.transform.position, AttackController.transform.position + transform.right * frontAttackDistance);
        Gizmos.DrawWireCube(OperadorSuelo.position, dimensionesCaja);
        Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio);
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(backController.transform.position, backController.transform.position + transform.right * backDistance);
    }
}