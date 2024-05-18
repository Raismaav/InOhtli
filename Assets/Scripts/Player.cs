using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] private float KBHitForece;

    [Header("Habilities settings")]
    [SerializeField] private GameObject HabBarGO;
    private HablityBarControllerSlider HabBar;

    [Header("Dash Settings")]
    [SerializeField] private float MaxCharge;
    [SerializeField] private float TimeCharge;
    
    private bool canDash=false;
    [SerializeField]private float DashTime;
    [SerializeField]private float DashSpeed,DashVerticalSpeed;
    [SerializeField]private Slider BarritaParaVerLaCarga;
    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        HabBar=HabBarGO.GetComponent<HablityBarControllerSlider>();
        setstartingValue(getCurrentHP());
        invincibleTime=invincibleDuration;
    }
   void Update()
    {
        if(invincibleTime>0){
            invincibleTime -= Time.deltaTime;
        }
        if(canMove){
            HorizontalMovement = Input.GetAxisRaw("Horizontal") * MoveSpeed;
            if (Input.GetButton("Jump"))
            {
                jump = true;
            }
            if (Input.GetButtonUp("Jump"))
            {
                cancelljump();
            }
            
            if(Input.GetButtonDown("Fire1") && TimeNextAttack <=0){
                Invoke("CharacterHit",AttackDuration);
                animator.SetTrigger("AttackTrigger");
                TimeNextAttack=TimeBetweenAttack;
            }
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
        if(Input.GetButton("Fire3")){
            if(TimeCharge<=MaxCharge){
                TimeCharge += Time.deltaTime;
                BarritaParaVerLaCarga.value= TimeCharge;
            }
        }
        if(Input.GetButtonUp("Fire3")){
            if(TimeCharge>=MaxCharge && canDash && HabBar.CanUse()){
                if(Input.GetButton("Vertical")){
                    StartCoroutine(Dash(DashVerticalSpeed,0.5f));
                }else
                StartCoroutine(Dash(0,1));
                HabBar.UseHabiliti();
                Debug.Log("Dasheo");
            }else{

                Debug.Log("No Dasheo");
            }
            TimeCharge=0;
            BarritaParaVerLaCarga.value= TimeCharge;
        }
        if(!Live){
            rb.velocityX=0;
            rb.velocityY=0;
            Destroy(gameObject);
        }
    }
    private void CharacterHit(){
        Collider2D[] Objects = Physics2D.OverlapCircleAll(AttackOperator.position, AttackRadio);
        foreach (Collider2D colition in Objects){
            if(colition.CompareTag("Enemigo")){
                colition.transform.GetComponent<HP>().Damage(AttackDamage,transform,KBHitForece);
            }
        }
    }
    private IEnumerator Dash(float Vertical, float HorizontalMod){
        canMove=false;
        canDash=false;
        float FixedSpeed;
        if(LD){
            FixedSpeed= DashSpeed*1*HorizontalMod;
        }else{
            FixedSpeed= DashSpeed*-1*HorizontalMod;
        }
        rb.gravityScale=0;
        rb.velocity=new Vector2(FixedSpeed,Vertical);
        //anim

        yield return new WaitForSeconds(DashTime);

        canMove=true;
        canDash=true;
        rb.gravityScale=1;
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
    public void unlockdash(){
        canDash=true;
        HabBarGO.SetActive(true);
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio);
        Gizmos.DrawWireCube(OperadorSuelo.position, dimensionesCaja);
    }
}
