using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestButtonAttack : MonoBehaviour
{
   [SerializeField] BasicDevelopEnemy bdeRAT,bdeBUHO;
    private OnMouseEnter Eveaf;

    void OnMouseDown()
    {
        bdeRAT.RatAttack();
        bdeBUHO.BuhoAttack();
    }
}
