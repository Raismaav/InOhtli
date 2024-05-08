using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//using Cinemachine;

public class flyEnemy : HP
{
    private CinemachineVirtualCamera cm;
    private SpriteRenderer sp;
    private Transform player;
    [SerializeField] private Transform retreat;
    public float detectionRatio = 15;
    private float distancex;
    private float distancey;
    private float distanceRetreatx,distanceRetreaty;
    

    public void Awake()
    {
        //cm = GameObject.FindGameObjectWithTag("VirtualCamera").GetComponent<CinemachineVirtualCamera>();
        sp = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        canFly=true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRatio);
        Gizmos.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        distancex = player.transform.position.x - transform.position.x;
        distancey = player.transform.position.y - transform.position.y;
        distanceRetreatx = retreat.transform.position.x - transform.position.x;
        distanceRetreaty = retreat.transform.position.y - transform.position.y;
        if(distancex < detectionRatio && distancex > -1*detectionRatio && distancey < detectionRatio && distancey > -1*detectionRatio){
            route(distancex,distancey);
        }else if(distanceRetreatx > 1 || distanceRetreatx < -1){
            route(distanceRetreatx,distanceRetreaty);
        }
        else {
            VerticalMovement=0;
            HorizontalMovement=0;
        }
        //HorizontalMovement = Input.GetAxisRaw("Horizontal") * MoveSpeed;
        //VerticalMovement = Input.GetAxisRaw("Vertical") * MoveSpeed;
        if(!Live){
            Destroy(gameObject);
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
    private void route(float distancex2,float distancey2){
        if(distancex2>0){
            HorizontalMovement = MoveSpeed;
        }else if(distancex2<0 ){
            HorizontalMovement = -1*MoveSpeed;
        }else HorizontalMovement=0;
        if(distancey2>0 ){
        VerticalMovement = MoveSpeed;
        }else if(distancey2<0 ){
            VerticalMovement = -1*MoveSpeed;
        }else VerticalMovement=0;
    }
}