using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestButtonAttack : MonoBehaviour
{
   [SerializeField] BasicDevelopEnemy bdeRAT,bdeBUHO,bdeJ;
    private OnMouseEnter Eveaf;

    void OnMouseDown()
    {
        bdeRAT.RatAttack();
        bdeBUHO.BuhoAttack();
        bdeJ.JaguarAttack();
    }
}
