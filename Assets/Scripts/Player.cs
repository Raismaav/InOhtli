using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : hpSystem
{
    [Header("Menu Setings")]
    [SerializeField] private bool InPause;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject InterfacePaused;
    [Header("attack Setings")]
    [SerializeField] private Transform AttackOperator;
    [SerializeField] private float AttackRadio;
    [SerializeField] private float AttackDamage;
    [SerializeField] private float TimeBetweenAttack;
    [SerializeField] private float TimeNextAttack;
    [SerializeField] private float AttackDuration;
    private Animator animator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        setstartingValue(getCurrentHP());
        //animator = GetComponent<Animator>();
    }
   void Update()
    {
        
        HorizontalMovement = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (!InPause) {
                Pause();
            }
            else
            {
                Resume();
            }
        }
        if(TimeNextAttack>0){
            TimeNextAttack -= Time.deltaTime;
        }
        if(Input.GetButtonDown("Fire1") && TimeNextAttack <=0){
            Invoke("CharacterHit",AttackDuration);
            //animator.SetTrigger("AttackTrigger");
            TimeNextAttack=TimeBetweenAttack;
        }
    }
    private void CharacterHit(){
        Collider2D[] Objects = Physics2D.OverlapCircleAll(AttackOperator.position, AttackRadio);
        foreach (Collider2D colition in Objects){
            if(colition.CompareTag("Enemigo")){
                colition.transform.GetComponent<HP>().Damage(AttackDamage);
            }
        }
    }
    private void Pause()
    {
        Time.timeScale = 0f;
        PauseMenu.SetActive(true);
        InterfacePaused.SetActive(false);
        InPause =true;
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
        InterfacePaused.SetActive(!InterfacePaused.activeSelf);
        InPause = false;
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio);
        Gizmos.DrawWireCube(OperadorSuelo.position, dimensionesCaja);
    }
}
