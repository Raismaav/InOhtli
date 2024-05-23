using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class FinalBoss : hpSystem
{
    [Header("Enemy Settings")]
    [SerializeField] private DoorController DoorsTiles;

    [Header("attack Setings")]
    [SerializeField] private float AttackDamage;
    [SerializeField] private float TimeBetweenAttack;
    [SerializeField] private float TimeNextAttack;
    [SerializeField] private float KBHitForece;
    private bool ActiveIA=false;
    [SerializeField] private Transform Player;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove=false;
        animator=GetComponent<Animator>();  
    }
    void Update()
    {
        float DistancePlayer=UnityEngine.Vector2.Distance(transform.position, Player.transform.position);
        animator.SetFloat("DistancePlayer",DistancePlayer);
        if(ActiveIA){
            
        }
        if(!Live){
            //trigger();
            Destroy(gameObject);
        }
    }
    /*private void CharacterHit(){
        Collider2D[] Objects = Physics2D.OverlapCircleAll(AttackOperator.position, AttackRadio);
        foreach (Collider2D colition in Objects){
            if(colition.CompareTag("Player")){
                colition.transform.GetComponent<HP>().Damage(AttackDamage,transform,KBHitForece);
                colition.transform.GetComponent<hpSystem>().hpBarChange();
                SoundController.Instance.SoundHurtPlay();
            }
        }
    }*/
    public void LockPlayer(){
        if((Player.position.x>transform.position.x&&!LD)||(Player.position.x<transform.position.x&&LD)){
            Turn();
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(OperadorSuelo.position, dimensionesCaja);
    }
    private void trigger(){
            DoorsTiles.OpenDoor2();
    }
    public void activate(){
        ActiveIA=true;
        canMove=true;
    }
}