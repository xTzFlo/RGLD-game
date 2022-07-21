using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Chest : MonoBehaviour
{
    public Transform chestTransform; 

    private bool isInZone = false;
    public Animator animChest;

    void Start()
    {
        
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && isInZone)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        animChest.SetTrigger("OpenChest");
        GetComponent<BoxCollider2D>().enabled = false;
        PhotonNetwork.Instantiate("coeur", new Vector3(chestTransform.position.x, chestTransform.position.y, 1), chestTransform.rotation, 0);


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Mage") || collision.CompareTag("Chevalier"))
        {
            isInZone = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Mage") || collision.CompareTag("Chevalier"))
        {
            isInZone = false;
        }
    }
}
