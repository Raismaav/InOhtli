using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBossHits : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.CompareTag("Player")){
            other.gameObject.GetComponent<HP>().Damage(1,transform,10);
            other.transform.GetComponent<hpSystem>().hpBarChange();
            SoundController.Instance.SoundHurtPlay();
        }
    }
}
