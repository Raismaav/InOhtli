using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using Cinemachine;

public class flyEnemy : MonoBehaviour
{
    private CinemachineVirtualCamera cm;
    private SpriteRenderer sp;
    private mov player;
    private Rigidbody2D rb;
    private bool forceAply;

    public float movementSpeed = 3;
    public float detectionRatio = 15;
    public LayerMask playerLayer;

    public Vector2 headPosition;

    public bool inHead;

    public int lifes = 3;
    public string namee;

    public void Awake()
    {
        cm = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        sp = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<mov>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameObject.name = namee;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRatio);
        Gizmos.color = Color.green;
        Gizmos.DrawCube((Vector2)transform.position + headPosition, new Vector2(1, 0.5f) * 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = player.transform.position - transform.position;
        float distance = Vector2.Distance(headPosition, player.transform.position);

        if (distance < detectionRatio)
        {
            rb.velocity = direction.normalized * movementSpeed;
            ChangeView(direction.normalized.x);
        } else
        {
            rb.velocity = Vector2.zero;
        }

        inHead = Physics2D.OverlapBox((Vector2)transform.position + headPosition, new Vector2(1, 0.5f) * 0.7f, 0, playerLayer);
    }

    private void ChangeView(float directionX)
    {
        if(directionX < 0 && transform.localScale.x > 0)
        {
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        }
        else if (directionX > 0 && transform.localScale.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            if(inHead)
            {
                player.GetComponent<Rigidbody2D>().velocity = Vector2.up * player.JumpForce;
                StartCoroutine(CameraDamage(0.1f));
                Destroy(gameObject, 0.2f);
            }
            else
            {
                //player.TakeDamage((transform.position - player.transform.position).normalized);
            }
        }
    }

    private void FixedUpdate()
    {
        if (forceAply)
        {
            rb.AddForce((transform.position - player.transform.position).normalized * 100, ForceMode2D.Impulse);
            forceAply = false;
        }
    }

    private void TakeDamage()
    {
        if(lifes > 0)
        {
            StartCoroutine(DamageEffect());
            StartCoroutine(CameraDamage(0.1f));
            forceAply = true;
            lifes--;
        } else
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
}
