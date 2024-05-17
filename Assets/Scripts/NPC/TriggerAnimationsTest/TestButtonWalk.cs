using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonWalk2 : MonoBehaviour
{
   
    [SerializeField] BasicDevelopEnemy bde;
    void OnMouseDown()
    {
        bde.RatDead();
    }
}
