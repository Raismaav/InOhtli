using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RatHitbox : MonoBehaviour
{
    [SerializeField] private float timeBetweenHits;
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            other.transform.GetComponent<HP>().Damage(1,transform,0);
            other.transform.GetComponent<hpSystem>().hpBarChange();
        }
    }
}
