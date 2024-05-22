using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonWalk : MonoBehaviour
{
    [SerializeField] BasicDevelopEnemy bde;
    [SerializeField] BasicDevelopEnemy bdeJ,bdeVENAO;
    void OnMouseDown()
    {
        bde.RarWalk();
        bdeJ.JaguarWalk();
        bdeVENAO.JaguarWalk();
    }
}
