using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.Pool;

public class ParabolArrow : HP
{
    public GameObject arrow;
    public Transform ShootController;
    public Transform Objetive;
    [SerializeField] private float h;
    private float gravity=-9.8f;
    public void shot(){
        GameObject arrowGO = Instantiate(arrow, ShootController.position, ShootController.rotation);
        Rigidbody2D rbArrow=arrow.gameObject.GetComponent<Rigidbody2D>();
        UnityEngine.Vector3 Speed= Objetive.position-transform.position;
        if(Speed.y<h){
            arrowGO.GetComponent<Rigidbody2D>().velocity=speedCalculate(Speed);
        }
    }
    private UnityEngine.Vector3 speedCalculate(UnityEngine.Vector3 Speed){
        float speedx,speedy;
        speedy=Mathf.Sqrt(-2*gravity*h);
        speedx=Speed.x/((-speedy/gravity)+Mathf.Sqrt(2*(Speed.y-h)/gravity));
        return new UnityEngine.Vector3(speedx,speedy,0);
    }
}
