using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FinalbossTriger : MonoBehaviour
{
    [SerializeField] private DoorController DoorsTiles;
    [SerializeField] private FinalBoss FinalBoss;
    private bool Activated;
    void OnTriggerEnter2D(Collider2D collider){
        if(!Activated){
            if(collider.CompareTag("Player")){
                DoorsTiles.FinalBossEnter();
                FinalBoss.activate();
                Activated=true;
            }
        }
    }
}
