using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorController : MonoBehaviour
    {
    private Tilemap doorTile;
    public Vector3Int pos;
    private bool closed;
    void Start(){
        doorTile=GetComponent<Tilemap>();
        pos.x=8;
        pos.y=-11;
        closed=true;
    }
    public void OpenDoor(){
        if(closed){
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y-2;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y-1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            closed=false;
        }   
    }
}
