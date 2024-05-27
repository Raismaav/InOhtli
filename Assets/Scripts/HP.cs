using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HP : mov
{
    // Start is called before the first frame update
    [Header("Health Settings")]
    [SerializeField]
    protected float CurrentHealth;
    [SerializeField] protected float MaxHealth;
    protected bool Live=true;
    public float invincibleTime;
    
    [SerializeField] protected float invincibleDuration;
    protected SpriteRenderer sp;
    [SerializeField] public GameObject Heal;
    [SerializeField] public GameObject DSprite;
    

    public void Damage(float DamageValue,Transform tr,float HitForce)
    {
        
        if(invincibleTime <= 0)
        {
            if (TrainingMode)
            {
                AddReward(-0.1f);
            }
            int fixedXDirection;
            CurrentHealth -= DamageValue;
            //Vector2 KnockbackDirection=transform.position-tr.position;
            if(!gameObject.CompareTag("Boss")){
                StartCoroutine(KBTime());
                if(transform.position.x-tr.transform.position.x>0){
                    fixedXDirection=1;
                }else{
                    fixedXDirection=-1;
                }
                rb.velocity=new Vector2(fixedXDirection*HitForce,rb.velocityY+0.5f);

                if(gameObject.CompareTag("Player"))SoundController.Instance.SoundHurtPlay();
            }
            if(sp!=null){
                StartCoroutine(DamageEffect());
            }
            invincibleTime=invincibleDuration;
        }
        if (CurrentHealth <= 0 )
        {
            if (TrainingMode)
            {
                AddReward(-0.5f);
                CurrentHealth = MaxHealth;
            }
            else
            {
                Live=false;
            }
        }
    }
    public void Cure(float CureValue)
    {
        if ((CurrentHealth + CureValue) > MaxHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
            CurrentHealth += CureValue;
        }
        if(sp!=null){
            StartCoroutine(CureEffect());
        }
    }
    private IEnumerator DamageEffect()
    {
        sp.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sp.color = Color.white;
    }
    private IEnumerator CureEffect()
    {
        sp.color = Color.green;
        yield return new WaitForSeconds(0.2f);
        sp.color = Color.white;
    }
    public float getCurrentHP(){
        return CurrentHealth;    
    }
    public float getMaxHP(){
        return MaxHealth;    
    }
    public void setCurrentHP(float newHP){
        CurrentHealth=newHP;
    }
    public void setMaxHP(float newMaxHP){
        MaxHealth=newMaxHP;
    }
    private IEnumerator KBTime(){
        canMove=false;
        yield return new WaitForSeconds(0.75f);
        canMove=true;
    }

}
