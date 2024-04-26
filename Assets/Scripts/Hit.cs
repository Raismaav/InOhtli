using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] private float timeBetweenHits;
    private float invincibleTime;
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            invincibleTime -= Time.deltaTime;
            if(invincibleTime <= 0)
            {
                other.GetComponent<hpSystem>().Damage(1);
                invincibleTime = timeBetweenHits;
            }
            
        }
    }
}
