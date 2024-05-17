using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonDie : MonoBehaviour
{
   
    [SerializeField] BasicDevelopEnemy bdeRAT,bdeBUHO;
    void OnMouseDown()
    {
        bdeRAT.RatDead();
        bdeBUHO.RatDead();
    }
}
