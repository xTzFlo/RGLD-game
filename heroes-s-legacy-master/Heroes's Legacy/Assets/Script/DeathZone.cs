using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    { 
         if (other.CompareTag("Chevalier"))
         {
             other.transform.position = new Vector2(-44, -37);
             PlayerHealth playerHealth = other.transform.GetComponent<PlayerHealth>();
             playerHealth.TakeDamage(25);
         }
 
         if (other.CompareTag("Mage"))
         {
             other.transform.position = new Vector2(-44, -37);
             PlayerHealth playerHealth = other.transform.GetComponent<PlayerHealth>();
             playerHealth.TakeDamage(25);
         }
         
                    
    }

    
}
