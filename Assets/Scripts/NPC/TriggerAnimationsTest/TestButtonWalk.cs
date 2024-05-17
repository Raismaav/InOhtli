using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonWalk : MonoBehaviour
{
    [SerializeField] BasicDevelopEnemy bde;
    void OnMouseDown()
    {
        bde.RarWalk();
    }
}
