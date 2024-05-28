using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSpriteMotion : MonoBehaviour
{
    private float fadeTime=1;
    private float fps;
    public float Alpha=1;
    [SerializeField] private SpriteRenderer sp;
    void Start(){
        fps=1/fadeTime;
        Alpha=sp.color.a;
    }
    void Update()
    {
        if(fadeTime>-0){
            Alpha-=fps*Time.deltaTime;
            gameObject.transform.position += Vector3.up * 0.01f;
            sp.color=new Color(sp.color.r, sp.color.g,sp.color.b,Alpha);
            if(Alpha<0){
                Destroy(gameObject);
            }
        }
    }
}
