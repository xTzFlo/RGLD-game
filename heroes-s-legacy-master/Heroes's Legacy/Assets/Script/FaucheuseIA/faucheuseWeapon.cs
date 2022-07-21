using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faucheuseWeapon : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Chevalier") || collision.transform.CompareTag("Mage"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(20);
        }
    }
}
