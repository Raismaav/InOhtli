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
    private bool Hit;

    void Start()
    {
        anim=GetComponent<Animator>();
        Application.targetFrameRate=60;
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
    //JAGUAR-----------------------------------
    public void JaguarAttack(){
        anim.SetTrigger("AttackTrigger");
        Invoke("hitPreview",AttackDuration);
    }
    public void JaguarJump(){
        if(anim.GetBool("Jump")){
            anim.SetBool("Jump",false);
        }else
        anim.SetBool("Jump",true);
    }
    public void JaguarFall(){
        if(anim.GetBool("Fall")){
            anim.SetBool("Fall",false);
        }else
        anim.SetBool("Fall",true);
    }
    public void JaguarWalk(){
        if(anim.GetInteger("Walk")!=0){
            anim.SetInteger("Walk",0);
        }else
        anim.SetInteger("Walk",1);
    }
    public void JaguarDead(){
        anim.SetTrigger("DIE");
    }
    //--------------------------------
    public void OnDrawGizmos(){
        if(Hit){
            Gizmos.color = Color.black;
            Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio*2);
            Hit=false;
        }else{
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio);
        }
    }
    public void hitPreview(){
        Hit = true;
        Debug.Log("hit");
    }
}
