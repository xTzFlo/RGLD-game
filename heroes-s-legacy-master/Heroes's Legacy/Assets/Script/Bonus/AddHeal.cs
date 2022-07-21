using System;
using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using UnityEngine;

public class AddHeal : MonoBehaviour
{
    public GameObject hearth;
    private float Time;
    private PhotonView PV;
    int id;
    private bool visu = false;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        id = GetComponent<PhotonView>().ViewID;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (visu)
            return;
        
        if (other.tag == "projectile" || other.tag == "Arme")
            return;
        
        int currentHealth = other.GetComponent<PlayerHealth>().currentHealth;

        if (currentHealth == 100 && other.tag == "Chevalier" || currentHealth == 80 && other.tag == "Mage")
            return;
        
        
        other.GetComponent<PlayerHealth>().HealthCare(20);
        hearth.GetComponent<Transform>().localScale = new Vector3(0.0000001f, 0.00000001f, 0.05f);
        visu = true;
        
        PV.RPC("Destroy", RpcTarget.MasterClient, id); 

    }


    [PunRPC] void Destroy(int viewid)
    {
        PhotonView todestroy = PhotonView.Find(viewid);
        PhotonNetwork.Destroy(todestroy);
    }
    
}
