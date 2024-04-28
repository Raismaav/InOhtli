using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Door : MonoBehaviour
{
    [SerializeField] private DoorController DoorsTiles;
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            DoorsTiles.OpenDoor();
        }
    }
}
