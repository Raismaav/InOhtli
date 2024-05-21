using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class HP : mov
{
    // Start is called before the first frame update
    [Header("Health Settings")]
    [SerializeField] private float CurrentHealth;
    [SerializeField] private float MaxHealth;
    protected bool Live=true;
    public float invincibleTime;
    
    [SerializeField] protected float invincibleDuration;
    

    public void Damage(float DamageValue,Transform tr,float HitForce)
    {
        if(invincibleTime <= 0)
        {
            int fixedXDirection;
            CurrentHealth -= DamageValue;
            //Vector2 KnockbackDirection=transform.position-tr.position;
            StartCoroutine(KBTime());
            if(transform.position.x-tr.transform.position.x>0){
                fixedXDirection=1;
            }else{
                fixedXDirection=-1;
            }
            rb.velocity=new Vector2(fixedXDirection*HitForce,rb.velocityY+0.5f);
            invincibleTime=invincibleDuration;
        }
        if (CurrentHealth <= 0 )
        {
            Live=false;
        }
    }

    public void Cure(float CureValue)
    {
        if ((CurrentHealth + CureValue) > CurrentHealth)
        {
            CurrentHealth = MaxHealth;
        }
        else
        {
            CurrentHealth += CureValue;
        }
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
        yield return new WaitForSeconds(1);
        canMove=true;
    }

}
