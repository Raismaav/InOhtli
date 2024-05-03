using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HablityBarControllerSlider : MonoBehaviour
{
    // Start is called before the first frame update
    private Slider slider;
    [SerializeField] private Slider chargeSlider;
    private float CurrentHab=0;
    public int HabCharges;
    [SerializeField]private int maxcharges=3;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    void FixedUpdate()
    {
        if(HabCharges<maxcharges){
            if(CurrentHab<=slider.maxValue){
                slider.value= CurrentHab;
                CurrentHab=CurrentHab+1*Time.deltaTime;
            }else{
                HabCharges++;
                CurrentHab=0;
                slider.value= CurrentHab;
                HabChargesValue();
            }
        }
    }
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            if(getCharges()>0){
                setCharges(getCharges()-1);
                HabChargesValue();
            }
        }
    }
    private void HabChargesValue(){
        chargeSlider.value=HabCharges;
    }
    public void setCharges(int newcharge){
        HabCharges=newcharge;
    }
    public void addCharges(int multiplier){
        HabCharges+=1*multiplier;
    }
    public int getCharges(){
        return HabCharges;
    }
    public void setMaxCharges(int newcharge){
        HabCharges=newcharge;
    }
}
