using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Archer : MonoBehaviour
{
    private mov player;
    private Rigidbody2D rb;
    private SpriteRenderer sp;
    private Animator anim;
    private CinemachineVirtualCamera cm;
    private bool forceApply;

    public float playerDistance = 17;
    public float arrowDistance = 11;
    public GameObject arrow;
    public float launchForce = 5;
    public float movementSpeed = 5;
    public float lifes = 3;
    public bool throwArrow;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<mov>();
        rb = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        cm = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = "Archer";
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.transform.position - rb.transform.position).normalized * arrowDistance;
        Debug.DrawRay(transform.position, direction, Color.red);

        float actualDistance = Vector2.Distance(transform.position, player.transform.position);

        if(actualDistance <= arrowDistance)
        {
            rb.velocity = Vector2.zero;
            anim.SetBool("Caminando", false);

            Vector2 normalizedDirection = direction.normalized;
            ChangeView(normalizedDirection.x);
            if(!throwArrow)
            {
                StartCoroutine(ThrowArrow(direction, actualDistance));
            }
            else
            {
                if(actualDistance <= playerDistance)
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
        if(directionX < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x,transform.localScale.y,transform.localScale.z);
        } else if(directionX > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, playerDistance);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, arrowDistance);
    }

    private IEnumerator ThrowArrow(Vector2 arrowDirection, float distance)
    {
        throwArrow = true;
        anim.SetBool("disparando", true);
        yield return new WaitForSeconds(1.42f);
        anim.SetBool("disparando", false);
        arrowDirection = arrowDirection.normalized;

        GameObject arrowGO = Instantiate(arrow, transform.position, Quaternion.identity);
        arrowGO.transform.GetComponent<Arrow>().arrowDirection = arrowDirection;
        arrowGO.transform.GetComponent<Arrow>().archer = this.gameObject;

        arrowGO.transform.GetComponent<Rigidbody2D>().velocity = arrowDirection * launchForce;
        throwArrow = false;
    }

    private void TakeDamage()
    {
        if (lifes > 0)
        {
            StartCoroutine(DamageEffect());
            StartCoroutine(CameraDamage(0.1f));
            forceApply = true;
            lifes--;
        }
        else
        {
            Destroy(gameObject, 0.2f);
        }
    }

    private IEnumerator CameraDamage(float time)
    {
        CinemachineBasicMultiChannelPerlin cinemachinebasicmultichannelperlin = cm.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        cinemachinebasicmultichannelperlin.m_AmplitudeGain = 5;
        yield return new WaitForSeconds(time);
        cinemachinebasicmultichannelperlin.m_AmplitudeGain = 0;
    }

    private IEnumerator DamageEffect()
    {
        sp.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        sp.color = Color.white;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
                //player.TakeDamage((transform.position - player.transform.position).normalized);
        }
    }

    private void FixedUpdate()
    {
        if (forceApply)
        {
            rb.AddForce((transform.position - player.transform.position).normalized * 100, ForceMode2D.Impulse);
            forceApply = false;
        }
    }
}
