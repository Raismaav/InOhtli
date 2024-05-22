using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButtonJump : MonoBehaviour
{
    [SerializeField] BasicDevelopEnemy bdeJ,bdeVENAO;
    void OnMouseDown()
    {
        bdeJ.JaguarJump();
        bdeVENAO.JaguarJump();
    }
}
