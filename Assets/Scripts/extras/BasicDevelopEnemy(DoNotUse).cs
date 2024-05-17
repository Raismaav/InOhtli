using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDevelopEnemy : MonoBehaviour
{
    Animator anim;
    [SerializeField] Animator PivotAnim;
    [Header("attack Setings")]
    [SerializeField] private Transform AttackOperator;
    [SerializeField] private float AttackRadio;
    [SerializeField] private float AttackDamage;
    [SerializeField] private float TimeBetweenAttack;
    [SerializeField] private float TimeNextAttack;
    [SerializeField] private float AttackDuration;

    void Start()
    {
        anim=GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*if(Input.GetButtonDown("Fire1")){
            anim.SetTrigger("AttackTrigger");
        }*/
    }
    //RAT---------------------------------------
    public void RatAttack(){
        anim.SetTrigger("AttackTrigger");
        PivotAnim.SetTrigger("Attack");
    }
    public void RarRun(){
        anim.SetTrigger("AttackTrigger");
    }
    public void RarWalk(){
        if(anim.GetBool("Walk")){
            anim.SetBool("Walk",false);
        }else
        anim.SetBool("Walk",true);
    }
    public void RatDead(){
        anim.SetTrigger("DIE");
    }
    //BUHO-------------------------------------
    public void BuhoAttack(){
        anim.SetTrigger("AttackTrigger");
    }
    public void BuhoFly(){
        if(anim.GetBool("Fly")){
            anim.SetBool("Fly",false);
        }else
        anim.SetBool("Fly",true);
    }
    public void BuhoDead(){
        anim.SetTrigger("DIE");
    }
    //-----------------------------------
    public void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio);
    }
}
