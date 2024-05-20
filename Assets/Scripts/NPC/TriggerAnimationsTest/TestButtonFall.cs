using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonFall : MonoBehaviour
{
    [SerializeField] BasicDevelopEnemy bdeJ;
    void OnMouseDown()
    {
        bdeJ.JaguarFall();
    }
}
