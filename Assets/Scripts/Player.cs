using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class Player : hpSystem
{
    [Header("Menu Setings")]
    [SerializeField] private bool InPause;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject InterfacePaused;
    [SerializeField] private GameObject DeathMenu;
    [SerializeField] private GameObject cam;
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
    [SerializeField]private ParticleSystem inchargeParticles;
    [SerializeField]private ParticleSystem chargedParticles;
    
    [Header("Sound Settings")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip AttackAudioClip;
    [SerializeField] private AudioClip walkCaveAudioClip;
    [SerializeField] private AudioClip walkGroundAudioClip;
    [SerializeField] private AudioClip HurtAudioClip;
    [SerializeField] private AudioClip JumpAudioClip;
    
    [SerializeField] private AudioClip DashAudioClip;
    private bool DashUnlocked;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        HabBar=HabBarGO.GetComponent<HablityBarControllerSlider>();
        setstartingValue(getCurrentHP());
        audioSource=GetComponent<AudioSource>();
    }
   void Update()
    {
        if(invincibleTime>0){
            invincibleTime -= Time.deltaTime;
        }
        if(canMove){
            HorizontalMovement = Input.GetAxisRaw("Horizontal") * MoveSpeed;
            animator.SetInteger("Walk",(int)HorizontalMovement);
            if(HorizontalMovement!=0 && inFloor){
                audioSource.mute=false;
            }else{
                audioSource.mute=true;
            }
            if (Input.GetButtonDown("Jump"))
            {
                animator.SetBool("Jump",true);
                jump = true;
                if(inFloor)
                SoundController.Instance.SoundPlay(JumpAudioClip);
            }
            if (Input.GetButtonUp("Jump"))
            {
                cancelljump();
            }
            
            if(Input.GetButtonDown("Fire1") && TimeNextAttack <=0){
                Invoke("CharacterHit",AttackDuration);
                animator.SetTrigger("AttackTrigger");
                SoundController.Instance.SoundPlay(AttackAudioClip);
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
            if(TimeCharge>=MaxCharge){
                if(inchargeParticles.isPlaying==true){
                    inchargeParticles.Stop();
                    chargedParticles.Play();
                }                
            }else if(TimeCharge<=MaxCharge&&canDash){
                if(TimeCharge<=0.01 && TimeCharge>=0){
                    inchargeParticles.Play();
                }
                TimeCharge += Time.deltaTime;
                BarritaParaVerLaCarga.value= TimeCharge;
            }
        }
        if(Input.GetButtonUp("Fire3")){
            if(chargedParticles.isPlaying==true){
                chargedParticles.Stop();
            }
            if(inchargeParticles.isPlaying==true){
                inchargeParticles.Stop();
            }
            if(TimeCharge>=MaxCharge && canDash && HabBar.CanUse()){
                if(Input.GetButton("Vertical")){
                    StartCoroutine(Dash(DashVerticalSpeed,0.5f));
                }else
                SoundController.Instance.SoundPlay(DashAudioClip);
                StartCoroutine(Dash(0,1));
                HabBar.UseHabiliti();
                Debug.Log("Dasheo");
            }else{

                Debug.Log("No Dasheo");
            }
            TimeCharge=0;
            BarritaParaVerLaCarga.value= TimeCharge;
        }
        if(!inFloor){
            if(rb.velocityY>0){
                animator.SetBool("Jump",true);
                animator.SetBool("Fall",false);
            }else{
                animator.SetBool("Jump",false);
                animator.SetBool("Fall",true);
            }

        }else{
                animator.SetBool("Fall",false);  
                animator.SetBool("Jump",false);
        }
        if(!Live){
            animator.SetTrigger("DIE");
            canMove=false;
            StartCoroutine(DeadTime());
        }
    }
    private void CharacterHit(){
        Collider2D[] Objects = Physics2D.OverlapCircleAll(AttackOperator.position, AttackRadio);
        foreach (Collider2D colition in Objects){
            if(colition.CompareTag("Enemigo")||colition.CompareTag("Boss")){
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
        animator.SetBool("Dash",true);

        yield return new WaitForSeconds(DashTime);
        animator.SetBool("Dash",false);
        canMove=true;
        canDash=true;
        rb.gravityScale=1;
    }
    private IEnumerator DeadTime(){
        yield return new WaitUntil(() => !animator.GetCurrentAnimatorStateInfo(0).IsName("DED"));
        deathMenu();
        gameObject.SetActive(false);
    }
    private void PlayParticles(){
        chargedParticles.Play();
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
        DashUnlocked=true;
    }
    public void deathMenu(){
        DeathMenu.SetActive(true);
        InterfacePaused.SetActive(false);
        InPause =true;
    }
    private void OnDrawGizmos(){
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(AttackOperator.position, AttackRadio);
        Gizmos.DrawWireCube(OperadorSuelo.position, dimensionesCaja);
    }
    public bool DashCheck(){
        return DashUnlocked;
    }
}
