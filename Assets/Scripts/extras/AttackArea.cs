using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemigo"))
        {
            Debug.Log("Aplicar daño a enemigo");
        }
    }
}
