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
    [SerializeField] public Transform Player;
    [SerializeField] public Animator HandAnimator;
    [SerializeField] private GameObject HB;
    [SerializeField] private AudioClip DedAudioClip;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        canMove=false;
        animator=GetComponent<Animator>();  
    }
    void Update()
    {
        if(ActiveIA){
            float DistancePlayer=UnityEngine.Vector2.Distance(transform.position, Player.transform.position);
            animator.SetFloat("DistancePlayer",DistancePlayer);
            hpBarChange();
            if(!Live){
                trigger();
                HB.SetActive(false);
                SoundController.Instance.SoundPlay(DedAudioClip);
                Destroy(gameObject);
            }
        }
    }
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
        animator.SetBool("ActiveIA",true);
        HB.SetActive(true);
        setstartingValue(getCurrentHP());
    }
    public void TriggerHitbox(){
        HandAnimator.SetTrigger("AttackTrigger");
    }
}