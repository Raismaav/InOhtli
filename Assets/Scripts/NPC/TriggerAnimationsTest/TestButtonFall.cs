using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonFall : MonoBehaviour
{
    [SerializeField] BasicDevelopEnemy bdeJ,bdeVENAO,Lizzard,FinalBoss;
    void OnMouseDown()
    {
        bdeJ.JaguarFall();
        bdeVENAO.JaguarFall();
        Lizzard.JaguarFall();
        FinalBoss.JaguarFall();
    }
}
