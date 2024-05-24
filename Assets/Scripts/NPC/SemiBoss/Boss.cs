using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Boss : hpSystem
{
    [Header("Enemy Settings")]
    public LayerMask frontLayer;
    public LayerMask PlayerLayer;
    public float frontDistance;
    public Transform frontController;
    public float front2;
    public Transform WallController;
    public bool frontInfo;
    public bool AttackRange;
    [SerializeField] private DoorController DoorsTiles;
    private BoxCollider2D BoxC;
    private CapsuleCollider2D CapsC;
    [SerializeField] private GameObject HitTriggerTongue;
    [SerializeField] private GameObject HitTriggerTail;

    [Header("attack Setings")]
    [SerializeField] private Transform AttackOperator;
    [SerializeField] private float AttackRadio;
    [SerializeField] private float AttackDamage;
    [SerializeField] private float TimeBetweenAttack;
    [SerializeField] private float TimeNextAttack;
    [SerializeField] private float AttackDuration;
    [SerializeField] private float KBHitForece;
    [SerializeField] public Transform Player;
    private bool ActiveIA=false;
    [SerializeField] private Player p;
    [SerializeField] public GameObject Dummy;
    [SerializeField] private GameObject HB;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove=false;
        animator=GetComponent<Animator>();
        BoxC=GetComponent<BoxCollider2D>();
        CapsC=GetComponent<CapsuleCollider2D>();
    }
    void Update()
    {
        if(ActiveIA){
            if(invincibleTime>0){
                invincibleTime -= Time.deltaTime;
            }
            float DistancePlayer=UnityEngine.Vector2.Distance(transform.position, p.transform.position);
            animator.SetFloat("DistancePlayer",DistancePlayer);
            hpBarChange();
            frontInfo = Physics2D.Raycast(WallController.position, transform.right, front2, frontLayer);
            AttackRange = Physics2D.Raycast(frontController.position, transform.right, frontDistance, PlayerLayer);
            animator.SetBool("AttackRange",AttackRange);
            animator.SetBool("Wall",frontInfo);
            
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("colazo")){
                HitTriggerTail.SetActive(true);
            }else{
                HitTriggerTail.SetActive(false);
            }
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("lenguetazo")){
                HitTriggerTongue.SetActive(true);
            }else{
                HitTriggerTongue.SetActive(false);
            }
            if(animator.GetCurrentAnimatorStateInfo(0).IsName("Garra")){
                CharacterHit();
            }
            
        }
        if(!Live){
            trigger();
            HB.SetActive(false);
            Destroy(gameObject);
        }
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
    public void LockPlayer(){
        if((p.transform.position.x>transform.position.x&&!LD)||(p.transform.position.x<transform.position.x&&LD)){
            Turn();
        }
    }
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<HP>().Damage(1,transform,10);
            other.transform.GetComponent<hpSystem>().hpBarChange();
        }
    }
    private void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<HP>().Damage(1,transform,10);
            other.transform.GetComponent<hpSystem>().hpBarChange();
        }
    }
    public void SetBox(bool Active){
        BoxC.enabled=Active;
    }
    public void SetTrigger1(bool Active){
        HitTriggerTongue.SetActive(Active);
    }
    public void SetTrigger2(bool Active){
        HitTriggerTail.SetActive(Active);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(frontController.transform.position, frontController.transform.position + transform.right * frontDistance);
        Gizmos.DrawLine(WallController.transform.position, WallController.transform.position + transform.right * front2);
        Gizmos.DrawWireCube(OperadorSuelo.position, dimensionesCaja);
        Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio);
    }
    private void trigger(){
        DoorsTiles.OpenDoor();
        p.unlockdash();
    }
    public void activate(){
        ActiveIA=true;
        animator.SetBool("ActiveIA",true);
        HB.SetActive(true);
        setstartingValue(getCurrentHP());
        setMaxHPValue();
    }
}