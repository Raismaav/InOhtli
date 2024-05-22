using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.MLAgents.Integrations.Match3;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D rb;
    void Start(){
        rb=GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        float angle=Mathf.Atan2(rb.velocityY,rb.velocityX)*Mathf.Rad2Deg;
        transform.rotation =Quaternion.AngleAxis(angle,Vector3.forward);
    }
    void OnCollisionEnter2D(Collision2D collision){
        Destroy(gameObject);
    }
}
