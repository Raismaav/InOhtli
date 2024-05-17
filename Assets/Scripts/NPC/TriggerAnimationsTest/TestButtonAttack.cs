using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TestButtonAttack : MonoBehaviour
{
   [SerializeField] BasicDevelopEnemy bde;
    private OnMouseEnter Eveaf;

    void OnMouseDown()
    {
        bde.RatAttack();
    }
}
