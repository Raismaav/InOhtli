using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTrigger : MonoBehaviour
{
    [SerializeField] private DataController DC;
    private bool InRange;


    // Update is called once per frame
    void Update()
    {
        if(InRange && Input.GetButtonDown("Fire1")){
            DC.DataSave();
        }
    }
    private void OnTriggerEnter2D(Collider2D col){
        if (col.gameObject.CompareTag("Player"))
        {
            InRange=true;
        }
    }
    private void OnTriggerExit2D(Collider2D col){
        if (col.gameObject.CompareTag("Player"))
        {
            InRange=false;
        }
    }
}
