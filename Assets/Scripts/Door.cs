using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorController DoorsTiles;
    private void trigger(){
        DoorsTiles.OpenDoor();
    }
}
