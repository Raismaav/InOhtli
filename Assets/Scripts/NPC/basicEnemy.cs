using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Serialization;

public class basicEnemy : HP
{
    [Header("Enemy Settings")]
    public LayerMask belowLayer;
    public LayerMask frontLayer;
    public LayerMask attackLayer;
    public float belowDistance;
    public float frontDistance;
    public Transform belowController;
    public Transform frontController;
    public bool belowInfoRat;
    public bool frontInfoRat;
    public bool attackinfoRat;
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
    [SerializeField] Animator PivotAnim;
    string str="";

    public override void Initialize()
    {
        rb = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        runspeed=HorizontalMovement;
        sp = GetComponent<SpriteRenderer>();
        if(!TrainingMode) MaxStep = 0;
    }

    public override void OnEpisodeBegin()
    {
        runspeed = HorizontalMovement;
        CurrentHealth = MaxHealth;
    }
    
    public override void CollectObservations(VectorSensor sensor)
    {
        frontInfoRat = Physics2D.Raycast(frontController.position, transform.right, frontDistance, frontLayer);
        attackinfoRat = Physics2D.Raycast(frontController.position, transform.right, frontDistance, attackLayer);
        belowInfoRat = Physics2D.Raycast(belowController.position, transform.up * -1, belowDistance, belowLayer);
        bool inFloorRat = inFloor;
        float invincibleTimeRat = invincibleTime;
        float CurrentHealthRat = CurrentHealth;
        sensor.AddObservation(frontInfoRat);
        sensor.AddObservation(attackinfoRat);
        sensor.AddObservation(belowInfoRat);
        sensor.AddObservation(inFloorRat);
        sensor.AddObservation(invincibleTimeRat);
        sensor.AddObservation(CurrentHealthRat);
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

        if(attack && TimeNextAttack <=0){
            if(PivotAnim!=null){
                animator.SetTrigger("AttackTrigger");
                PivotAnim.SetTrigger("Attack");
            }
            else{
                Invoke("CharacterHit",AttackDuration);
            }
            TimeNextAttack=TimeBetweenAttack;
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
        frontInfoRat = Physics2D.Raycast(frontController.position, transform.right, frontDistance, frontLayer);
        attackinfoRat = Physics2D.Raycast(frontController.position, transform.right, frontDistance, attackLayer);
        belowInfoRat = Physics2D.Raycast(belowController.position, transform.up * -1, belowDistance, belowLayer);
        animator.SetInteger("Walk",(int)HorizontalMovement);
        if(TimeNextAttack>0){
            TimeNextAttack -= Time.deltaTime;
        }
        if(attackinfoRat && TimeNextAttack <=0){
            if(PivotAnim!=null){
                animator.SetTrigger("AttackTrigger");
                PivotAnim.SetTrigger("Attack");
            }
            else{
                Invoke("CharacterHit",AttackDuration);
            }
            TimeNextAttack=TimeBetweenAttack;
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
        if(frontInfoRat || !belowInfoRat)
        {
            //Girar
            Girar();
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
                if (TrainingMode)
                {
                    AddReward(0.5f);
                }
                colition.transform.GetComponent<HP>().Damage(AttackDamage,transform,KBHitForece);
                colition.transform.GetComponent<hpSystem>().hpBarChange();
            }
        }
    }
    private IEnumerator animWait(){
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName(str));
        canMove=true;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(belowController.transform.position, belowController.transform.position + transform.up * -1 *  belowDistance);
        Gizmos.DrawLine(frontController.transform.position, frontController.transform.position + transform.right * frontDistance);
        Gizmos.DrawWireCube(OperadorSuelo.position, dimensionesCaja);
        Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio);
    }
}