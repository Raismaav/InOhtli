using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinalbossTriger : MonoBehaviour
{
    [SerializeField] private DoorController DoorsTiles;
    [SerializeField] private FinalBoss FinalBoss;
    void OnTriggerEnter2D(Collider2D collider){
       if(collider.CompareTag("Player")){
            DoorsTiles.FinalBossEnter();
            FinalBoss.activate();
        }
    }
}
