using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.XR;

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
    public bool belowInfo;
    public bool frontInfo;
    public bool attackinfo;
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


    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator=GetComponent<Animator>();
        runspeed=HorizontalMovement;
    }
    void Update()
    {
        if(invincibleTime>0){
            invincibleTime -= Time.deltaTime;
        }
        //Move(HorizontalMovement,false);
        frontInfo = Physics2D.Raycast(frontController.position, transform.right, frontDistance, frontLayer);
        attackinfo = Physics2D.Raycast(frontController.position, transform.right, frontDistance, attackLayer);
        belowInfo = Physics2D.Raycast(belowController.position, transform.up * -1, belowDistance, belowLayer);
        animator.SetInteger("Walk",(int)HorizontalMovement);
        if(TimeNextAttack>0){
            TimeNextAttack -= Time.deltaTime;
        }
        if(attackinfo && TimeNextAttack <=0){
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
        if(frontInfo || !belowInfo)
        {
            //Girar
            Girar();
        }
        if(!Live){
            Destroy(gameObject);
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
                SoundController.Instance.SoundHurtPlay();
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