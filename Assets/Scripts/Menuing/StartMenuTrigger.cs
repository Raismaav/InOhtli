using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuTrigger : MonoBehaviour
{
    [SerializeField] Image tx;
    [SerializeField]private float fadeTime;
    private float fps;
    public float Alpha;
    private bool fadded=false;
    void Start(){
        fps=1/fadeTime;
        Alpha=tx.color.a;
    }
    void Update()
    {
        if (Input.anyKey)
        {
            Debug.Log("A key or mouse click has been detected");
            SceneManager.LoadScene("FileMenu");
        }
        if(!fadded){
            if(fadeTime>-0.5){
                Alpha-=fps*Time.deltaTime;
                //if(Alpha<0){
                    tx.color=new Color(tx.color.r, tx.color.g,tx.color.b,Alpha);
                //}
                fadeTime-=Time.deltaTime;
            }else fadded = true;
        }else if(fadded){
            if(fadeTime<2){
                Alpha+=fps*Time.deltaTime;
                //if(Alpha>0.9){
                    tx.color=new Color(tx.color.r, tx.color.g,tx.color.b,Alpha);
                //}
                fadeTime+=Time.deltaTime;
            }else fadded=false;
        }

    }
}
