using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using Cinemachine;

public class flyEnemy : HP
{
    private CinemachineVirtualCamera cm;
    private Transform player;
    [SerializeField] private Transform retreat;
    public float detectionRatio = 15;
    private float distancex;
    private float distancey;
    private float distanceRetreatx,distanceRetreaty;

    public LayerMask attackLayer;
    public Transform frontController;
    public float frontDistance;
    public bool attackinfo;

    [Header("attack Setings")]
    [SerializeField] private Transform AttackOperator;
    [SerializeField] private float AttackRadio;
    [SerializeField] private float AttackDamage;
    [SerializeField] private float TimeBetweenAttack;
    [SerializeField] private float TimeNextAttack;
    [SerializeField] private float AttackDuration;
    [SerializeField] private float KBHitForece;
    [SerializeField] Animator PivotAnim;

    

    public void Awake()
    {
        //cm = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        sp = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator=GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        canFly=true;
        sp=GetComponent<SpriteRenderer>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRatio);
        Gizmos.DrawLine(frontController.transform.position, frontController.transform.position + transform.right * frontDistance);
        Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio);
    }

    // Update is called once per frame
    void Update()
    {
        if(invincibleTime>0){
            invincibleTime -= Time.deltaTime;
        }
        attackinfo = Physics2D.Raycast(frontController.position, transform.right, frontDistance, attackLayer);
        if(!player.IsDestroyed()){
            distancex = player.transform.position.x - transform.position.x;
            distancey = player.transform.position.y - transform.position.y;
            distanceRetreatx = retreat.transform.position.x - transform.position.x;
            distanceRetreaty = retreat.transform.position.y - transform.position.y;
            if(distancex < detectionRatio && distancex > -1*detectionRatio && distancey < detectionRatio && distancey > -1*detectionRatio){
                route(distancex,distancey+1);
            }else if(distanceRetreatx > 1 || distanceRetreatx < -1){
                route(distanceRetreatx,distanceRetreaty);
            }
            else {
                VerticalMovement=0;
                HorizontalMovement=0;
            }
        }
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
                animator.SetTrigger("AttackTrigger");
            }
            TimeNextAttack=TimeBetweenAttack;
        }
        //HorizontalMovement = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        //VerticalMovement = Input.GetAxisRaw("Vertical") * MoveSpeed;
        if(!Live){
            canMove=false;
            rb.velocity=new Vector2(0,0);
            animator.SetTrigger("DIE");
        }
    }

    private IEnumerator CameraDamage(float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachinebasicmultichannelperlin = cm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachinebasicmultichannelperlin.m_AmplitudeGain = 5;
        yield return new WaitForSeconds(time);
        cinemachinebasicmultichannelperlin.m_AmplitudeGain = 0;
    }

    
    private void route(float distancex2,float distancey2){
        if(distancex2>0){
            HorizontalMovement = MoveSpeed;
        }else if(distancex2<0 ){
            HorizontalMovement = -1*MoveSpeed;
        }else HorizontalMovement=0;
        if(distancey2>0 ){
        VerticalMovement = MoveSpeed;
        }else if(distancey2<0 ){
            VerticalMovement = -1*MoveSpeed;
        }else VerticalMovement=0;
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
}