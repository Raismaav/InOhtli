using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SemibossTriger : MonoBehaviour
{
    [SerializeField] private DoorController DoorsTiles;
    [SerializeField] private Boss SemiBoss;
    private bool Activated;
    void OnTriggerEnter2D(Collider2D collider){
        if(!Activated){
            if(collider.CompareTag("Player")){
                DoorsTiles.semiBossEnter();
                SemiBoss.activate();
            }
        }
    }
}
