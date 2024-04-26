using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hpSystem : HP
{
    [SerializeField] private HealthBar HealthBar;

    public void hpBarChange(){
        HealthBar.ChangeCurrentHealth(getCurrentHP());
    }

    public void setstartingValue(float startingValue){
        HealthBar.SetStarterValue(getCurrentHP());
    }
}
