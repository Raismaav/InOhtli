using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    private Rigidbody2D rb;
    private BoxCollider2D bc;

    public LayerMask floorLayer;
    public GameObject archer;
    public Vector2 arrowDirection;
    public float collisionRadius = 0.25f;
    public bool touchingFloors;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //collision.GetComponent<PlayerController>().TakeDamage(-(collision.transform.position - archer.transform.position).normalized);
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        touchingFloors = Physics2D.OverlapCircle((Vector2)transform.position, collisionRadius, floorLayer);
        if (touchingFloors)
        {
            rb.bodyType = RigidbodyType2D.Static;
            bc.enabled = false;
            this.enabled = false;
        }

        float angle = Mathf.Atan2(arrowDirection.y, arrowDirection.x) * Mathf.Rad2Deg;

        transform.localEulerAngles = new Vector3(transform.localEulerAngles.y, transform.localEulerAngles.x, angle);
    }
}
