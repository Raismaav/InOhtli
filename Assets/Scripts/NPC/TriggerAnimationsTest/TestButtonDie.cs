using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonDie : MonoBehaviour
{
   
    [SerializeField] BasicDevelopEnemy bdeRAT,bdeBUHO,bdeJ,Lizzard,FinalBoss;
    void OnMouseDown()
    {
        bdeRAT.RatDead();
        bdeBUHO.RatDead();
        bdeJ.JaguarDead();
        Lizzard.JaguarDead();
        FinalBoss.JaguarDead();
    }
}
