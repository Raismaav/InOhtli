using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControlMenu : MonoBehaviour
{
    [SerializeField] GameObject IGMenu;
    [SerializeField] GameObject DashDetail;
    [SerializeField] Player P;
    public bool dash=false;
    void OnEnable()
    {
        dash=P.DashCheck();
        if(dash){
            DashDetail.SetActive(true);
        }else{
            DashDetail.SetActive(false);
        }
        
    }
    public void ControlMenuReturn(){
        IGMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
