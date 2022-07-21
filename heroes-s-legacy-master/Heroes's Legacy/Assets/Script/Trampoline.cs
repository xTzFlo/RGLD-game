using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    Rigidbody2D rb;
    public float launchForce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Mage") || collision.gameObject.CompareTag("Chevalier"))
        {
            rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.up * launchForce;
        }
    }
}
