using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movements : MonoBehaviour
{
    // Start is called before the first frame update
    public float JumpForce;
    public float Speed;
    
    private Rigidbody2D Rigidbody2D;
    private float Horizontal;
    private bool Grounded;
    
    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        Grounded = Physics2D.Raycast(transform.position, Vector3.down, 1f);
            
        if (Input.GetKeyDown(KeyCode.Space) && Grounded)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        Rigidbody2D.velocity = new Vector2(Horizontal * Speed, Rigidbody2D.velocity.y);
    }

    private void Jump()
    {
        Rigidbody2D.AddForce (Vector2.up * JumpForce);
    }
}
