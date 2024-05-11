using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicDevelopEnemy : MonoBehaviour
{
    Animator anim;
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
        if(Input.GetButtonDown("Fire1")){
            anim.SetTrigger("AttackTrigger");
        }
    }
    private void RatAttack(){

    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio);
    }
}
