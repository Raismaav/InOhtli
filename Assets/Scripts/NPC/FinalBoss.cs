using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBoss : hpSystem
{
    [Header("Enemy Settings")]
    public LayerMask belowLayer;
    public LayerMask frontLayer;
    public float belowDistance;
    public float frontDistance;
    public Transform belowController;
    public Transform frontController;
    public bool belowInfo;
    public bool frontInfo;
    private bool LookToRight = false;
    [SerializeField] private DoorController DoorsTiles;

    [Header("attack Setings")]
    [SerializeField] private Transform AttackOperator;
    [SerializeField] private float AttackRadio;
    [SerializeField] private float AttackDamage;
    [SerializeField] private float TimeBetweenAttack;
    [SerializeField] private float TimeNextAttack;
    [SerializeField] private float AttackDuration;
    [SerializeField] private float KBHitForece;
    private bool ActiveIA=false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove=false;
    }
    void Update()
    {
        if(ActiveIA){
            if(invincibleTime>0){
                invincibleTime -= Time.deltaTime;
            }
            //Move(HorizontalMovement,false);
            frontInfo = Physics2D.Raycast(frontController.position, transform.right, frontDistance, frontLayer);
            belowInfo = Physics2D.Raycast(belowController.position, transform.up * -1, belowDistance, belowLayer);
            if(TimeNextAttack>0){
                TimeNextAttack -= Time.deltaTime;
            }
            if(frontInfo && TimeNextAttack <=0){
                Invoke("CharacterHit",AttackDuration);
                //animator.SetTrigger("AttackTrigger");
                TimeNextAttack=TimeBetweenAttack;
            }
            if(frontInfo || !belowInfo)
            {
                //Girar
                Girar();
            }
        }
        if(!Live){
            trigger();
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
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(belowController.transform.position, belowController.transform.position + transform.up * -1 *  belowDistance);
        Gizmos.DrawLine(frontController.transform.position, frontController.transform.position + transform.right * frontDistance);
        Gizmos.DrawWireCube(OperadorSuelo.position, dimensionesCaja);
        Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio);
    }
    private void trigger(){
        DoorsTiles.OpenDoor2();
    }
    public void activate(){
        ActiveIA=true;
        canMove=true;
    }
}
