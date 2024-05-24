using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    private Slider slider;
    private float startingValue;
    private void Start()
    {
        slider = GetComponent<Slider>();
        slider.value=startingValue;
    }

    public void ChangeCurrentHealth(float CurrentHP)
    {
        slider.value= CurrentHP;
    }
    public void StartHealthBar(float HPValue)
    {
        ChangeCurrentHealth(HPValue);
    }

    public void SetStarterValue(float SetStarter){
        startingValue=SetStarter;
    }
    public void setMaxValue(float Setmax){
        slider.maxValue=Setmax;
    }
}