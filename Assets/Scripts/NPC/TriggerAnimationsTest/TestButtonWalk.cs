using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonWalk : MonoBehaviour
{
    [SerializeField] BasicDevelopEnemy bde;
    [SerializeField] BasicDevelopEnemy bdeJ,bdeVENAO,Lizzard,FinalBoss;
    void OnMouseDown()
    {
        bde.RarWalk();
        bdeJ.JaguarWalk();
        bdeVENAO.JaguarWalk();
        Lizzard.JaguarWalk();
        FinalBoss.JaguarWalk();
    }
}
