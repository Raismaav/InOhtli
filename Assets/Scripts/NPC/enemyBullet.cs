using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyBullet : MonoBehaviour
{
    public float bulletSpeed;
    public int damageInput;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        transform.Translate(Time.deltaTime * bulletSpeed * Vector2.left);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        /*if (other.GetComponent(out Hit lifeP))
        {
            lifeP.takeDamage(damageInput);
            Destroy(gameObject);
        }*/
        Destroy(gameObject);
    }
}
