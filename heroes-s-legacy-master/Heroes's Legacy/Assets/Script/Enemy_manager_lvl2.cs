using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class Enemy_manager_lvl2 : MonoBehaviour
{
    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("Platform_2"), new Vector2(-21.25F,-36.30F), Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("Platform_2"), new Vector2(-7F,-36.39F), Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("Platform_2"), new Vector2(5.49F,-36.39F), Quaternion.identity);
    }

    private void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            CreateController();
        }
                
    }
    
}