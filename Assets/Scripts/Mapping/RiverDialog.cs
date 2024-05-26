using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RiverDialog : MonoBehaviour
{
    [SerializeField] private TMP_Text LogText;
    [SerializeField] private GameObject Container;
    
    private bool FisrtPlay;
    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player"))
        {
            Container.SetActive(true);
            LogText.text="Este es el rio del que vine, ya no tengo nada que hacer ahi";
        }
    }
    private void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.CompareTag("Player"))
        {
            LogText.text="";
            Container.SetActive(false);
        }
    }
}
