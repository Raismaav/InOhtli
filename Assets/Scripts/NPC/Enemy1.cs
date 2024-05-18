using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : HP
{
    [Header("attack Setings")]
    [SerializeField] private Transform AttackOperator;
    [SerializeField] private float AttackRadio;
    [SerializeField] private float AttackDamage;
    [SerializeField] private float TimeBetweenAttack;
    [SerializeField] private float TimeNextAttack;
    [SerializeField] private float AttackDuration;
    [SerializeField] private float KBHitForece;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        
        HorizontalMovement = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if(TimeNextAttack>0){
            TimeNextAttack -= Time.deltaTime;
        }
        if(Input.GetKeyDown("t") && TimeNextAttack <=0){
            Invoke("CharacterHit",AttackDuration);
            //animator.SetTrigger("AttackTrigger");
            TimeNextAttack=TimeBetweenAttack;
        }
        if(!Live){
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
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio);
        Gizmos.DrawWireCube(OperadorSuelo.position, dimensionesCaja);
    }
}
