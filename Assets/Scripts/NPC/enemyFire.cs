using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class enemyFire : MonoBehaviour
{
    private mov mov;
    private Rigidbody2D rb;
    public Transform controladorDisparo;
    public float lineDistance;
    public LayerMask playerLayer;
    public bool playerInRange;
    public float timeBetween;
    public float timeLast;
    public GameObject enemyBullet;
    public float waitTime;
    private CinemachineVirtualCamera cm;
    private Animator anim;
    public float movementSpeed = 5;

    public float playerDistance = 14;
    public float shotDistance = 8;


    public void Awake()
    {
        mov = GameObject.FindGameObjectWithTag("Player").GetComponent<mov>();
        rb = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
        gameObject.name = "shotter";
    }

    // Update is called once per frame
    private void Update()
    {
        Vector2 direction = (mov.transform.position - rb.transform.position).normalized * shotDistance;
        Debug.DrawRay(transform.position, direction, Color.red);

        float actualDistance = Vector2.Distance(transform.position, mov.transform.position);

        if (actualDistance <= shotDistance)
        {
            //Disparo
            playerInRange = Physics2D.Raycast(controladorDisparo.position, transform.right, lineDistance, playerLayer);
            //Movimiento
            rb.velocity = Vector2.zero;
            anim.SetBool("Caminando", false);

            Vector2 normalizedDirection = direction.normalized;
            ChangeView(normalizedDirection.x);
            if (!enemyBullet.activeSelf)
            {
                if (!enemyBullet.activeSelf)
                {
                    enemyBullet.SetActive(true);
                }
                if (Time.time > timeBetween + timeLast)
                {
                    timeLast = Time.time;
                    Invoke(nameof(Shot), waitTime);

                }
            }
            else
            {
                if (actualDistance <= playerDistance)
                {
                    Vector2 movement = new Vector2(direction.x, 0);
                    movement = movement.normalized;
                    rb.velocity = movement * movementSpeed;
                    anim.SetBool("Caminando", true);
                    ChangeView(movement.x);
                }
                else
                {
                    anim.SetBool("Caminando", false);
                }
            }
        }

        
    }

    private void ChangeView(float directionX)
    {
        if (directionX < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (directionX > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void Shot()
    {
        Instantiate(enemyBullet, controladorDisparo.position, controladorDisparo.rotation);
    }

    private void OnDrawGizmos()
    {
        //Gizmos.color = Color.red;
        //Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * lineDistance);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, shotDistance);
    }
}
