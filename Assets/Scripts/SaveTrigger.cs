using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    [SerializeField] private DataController DC;
    private bool InRange;
    [SerializeField] private TMP_Text LogText;
    [SerializeField] private GameObject Container;


    // Update is called once per frame
    void Update()
    {
        if(InRange && Input.GetButtonDown("Fire1")){
            DC.DataSave();
            LogText.text="Partida Guardada";
        }
    }
    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player"))
        {
            InRange=true;
            Container.SetActive(true);
            LogText.text="Haz click para guardar";
        }
    }
    private void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.CompareTag("Player"))
        {
            InRange=false;
            LogText.text="";
            Container.SetActive(false);
        }
    }
}
