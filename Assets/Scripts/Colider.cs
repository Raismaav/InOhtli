using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Collider2D z_Collider;
    // Start is called before the first frame update
    void Start()
    {
        z_Collider = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
