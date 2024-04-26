using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : mov
{
    // Start is called before the first frame update
    [Header("Health Settings")]
    [SerializeField] private float CurrentHealth;
    [SerializeField] private float MaxHealth;

    public void Damage(float DamageValue)
    {
        CurrentHealth -= DamageValue;
        Debug.Log(CurrentHealth);
        if (CurrentHealth <= 0 )
        {
            Destroy(gameObject);
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

}
