using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mov : MonoBehaviour
{
    [Header("Movement Settings")]
    public Rigidbody2D rb;
    private Animator animator;
    public float HorizontalMovement = 0f;
    [SerializeField] public float MoveSpeed;
    [SerializeField] private float Smooth;
    private Vector3 velocidad = Vector3.zero;
    private bool LD = true;


    [SerializeField] public float JumpForce;
    [SerializeField] private LayerMask Floor;
    [SerializeField] public Transform OperadorSuelo;
    [SerializeField] public Vector3 dimensionesCaja;
    [SerializeField] private bool inFloor;
    public bool jump=false;

    

    
    private void FixedUpdate()
    {
        inFloor = Physics2D.OverlapBox(OperadorSuelo.position, dimensionesCaja, 0f, Floor);
        Move(HorizontalMovement*Time.deltaTime,jump);
        jump = false;
    }

    public void Move(float move,bool jump)
    {
        Vector3 velocidadObjetivo = new Vector2(move,rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, velocidadObjetivo,ref velocidad,Smooth);

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
}
