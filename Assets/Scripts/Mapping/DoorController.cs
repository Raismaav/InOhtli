using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DoorController : MonoBehaviour
    {
    private Tilemap doorTile;
    public Vector3Int pos;
    private bool closed=true;
    private bool closed2=true;
    public Tile t;
    public Tile t2;
    void Awake(){
        doorTile=GetComponent<Tilemap>();
    }
    public void OpenDoor(){
        if(closed){
            pos.x=8;
            pos.y=-11;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y-2;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y-1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.x=134;
            pos.y=12;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);

            pos.x=-1;
            pos.y=45;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.x=20;
            pos.y=50;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            closed=false;
        }   
    }
    public void OpenDoor2(){
        if(closed2){
            pos.x=273;
            pos.y=101;
            for(int x=273; x<278; x++){
                for(int y=101; y<104; y++){
                    pos.x=x;
                    pos.y=y;
                    doorTile.SetTile(doorTile.WorldToCell(pos), null);
                }
            }
            
            pos.x=255;
            pos.y=101;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.x=209;
            pos.y=101;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), null);

            closed2=false;
        }   
    }
    public void semiBossEnter(){
        if(closed){
            pos.x=-1;
            pos.y=45;
            doorTile.SetTile(doorTile.WorldToCell(pos), t);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), t);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), t);
            pos.x=20;
            pos.y=49;
            doorTile.SetTile(doorTile.WorldToCell(pos), t);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), t);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), t);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), t);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), t);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), t);
        }
    }
    public void FinalBossEnter(){
        if(closed2){
            pos.x=255;
            pos.y=101;
            doorTile.SetTile(doorTile.WorldToCell(pos), t2);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), t2);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), t2);
            pos.x=209;
            pos.y=101;
            doorTile.SetTile(doorTile.WorldToCell(pos), t2);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), t2);
            pos.y=pos.y+1;
            doorTile.SetTile(doorTile.WorldToCell(pos), t2);
        }
    }
}
