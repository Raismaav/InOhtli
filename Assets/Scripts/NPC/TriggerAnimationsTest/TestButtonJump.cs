using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonJump : MonoBehaviour
{
    [SerializeField] BasicDevelopEnemy bdeJ;
    void OnMouseDown()
    {
        bdeJ.JaguarJump();
    }
}
