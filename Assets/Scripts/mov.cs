using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;

public class mov : Agent
{
    [Header("Movement Settings")]
    protected Rigidbody2D rb;
    protected Animator animator;
    [SerializeField]protected float HorizontalMovement;
    protected float VerticalMovement = 0f;
    [SerializeField] protected float MoveSpeed;
    [SerializeField] private float Smooth;
    private Vector3 velocidad = Vector3.zero;
    protected bool LD = true;
    [SerializeField] protected bool canFly=false;


    [SerializeField] protected float JumpForce;
    [SerializeField] private LayerMask Floor;
    [SerializeField] protected Transform OperadorSuelo;
    [SerializeField] protected Vector3 dimensionesCaja;
    [SerializeField] protected bool inFloor;
    protected bool jump=false;
    
    public bool canMove=true;

    [Header("AI Setings")]
    [SerializeField]
    protected bool TrainingMode = false;

    
    private void FixedUpdate()
    {   
        if(canMove){
            inFloor = Physics2D.OverlapBox(OperadorSuelo.position, dimensionesCaja, 0f, Floor);
            Move(HorizontalMovement*Time.deltaTime,jump);
            if(canFly){
                MoveVerticaly(VerticalMovement*Time.deltaTime);
            }

            jump = false;
        }
    }

    public void Move(float move,bool jump)
    {
        if(canMove){
            Vector3 velocidadObjetivo = new Vector2(move,rb.velocity.y);
            rb.velocity = Vector3.SmoothDamp(rb.velocity, velocidadObjetivo,ref velocidad,Smooth);
        }
        if (move>0 && !LD)
        {
            Turn();
        }
        else if(move<0 && LD)
        {
            Turn();
        }
        if(inFloor && jump)
        {
            Jump();
        }
    }
    public void MoveVerticaly(float moveV)
    {
        Vector3 velocidadObjetivo = new Vector2(rb.velocity.x,moveV);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, velocidadObjetivo,ref velocidad,Smooth);
    }
    public void TurnRigth()
    {
        LD = !LD;
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
    }
    public void TurnLeft()
    {
        LD=!LD;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }
    public void Turn()
    {   
        LD=!LD;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
    }
    public void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.velocity += Vector2.up * JumpForce;
    }
    public void cancelljump(){
        if(rb.velocityY>2){
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y*.5f);
        }
    }
}