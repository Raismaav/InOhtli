using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEngine.Events;
using System;

public class btn1 : Selectable, ISelectHandler, IDeselectHandler
{
    public GameObject selectImage;
    public bool select;
    public bool GetIsHighlighted()
    {
        return IsHighlighted();
    }
    void ISelectHandler.OnSelect(BaseEventData eventData){
        select=true;
    }
    void IDeselectHandler.OnDeselect(BaseEventData eventData){
        select = false;
    }
    
    void Update(){
        if((GetIsHighlighted()||select)==true){
            selectImage.SetActive(true);
        }
        else if((GetIsHighlighted()&&select)==false && selectImage.activeSelf==true){
            selectImage.SetActive(false);
        }
    }
}
