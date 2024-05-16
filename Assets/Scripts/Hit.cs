using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    [SerializeField] private float timeBetweenHits;
    public float invincibleTime;
    void Start(){
        invincibleTime=timeBetweenHits;
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player"))
        {
            invincibleTime -= Time.deltaTime;
            if(invincibleTime <= 0)
            {
                other.GetComponent<HP>().Damage(10);
                invincibleTime = timeBetweenHits;
            }
            
        }
    }
    private void OnTriggerExit2D(Collider2D other){
        invincibleTime=timeBetweenHits;
    }
}
