using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonJump : MonoBehaviour
{
    [SerializeField] BasicDevelopEnemy bdeJ,bdeVENAO,Lizzard,FinalBoss;
    void OnMouseDown()
    {
        bdeJ.JaguarJump();
        bdeVENAO.JaguarJump();
        Lizzard.JaguarJump();
        FinalBoss.JaguarJump();
    }
}
