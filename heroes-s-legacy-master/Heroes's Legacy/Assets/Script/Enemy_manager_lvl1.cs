using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;


public class Enemy_manager_lvl1 : MonoBehaviour
{
    void CreateController()
    {
        PhotonNetwork.Instantiate(Path.Combine("slime_01"), new Vector2(-35.5F,-37),
            Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("slime_02"), new Vector2(-21,-36),Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("slime_03"), new Vector2(-3,-36),Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("slime_04"), new Vector2(20,-36),Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("Platform"), new Vector2(-31,-37),Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("Platform"), new Vector2(-15,-38),Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("Platform"), new Vector2(-5.52F,-36),Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("Platform"), new Vector2(3,-37.36F),Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("Platform"), new Vector2(10,-37.3F),Quaternion.identity);
        PhotonNetwork.Instantiate(Path.Combine("Platform"), new Vector2(17,-37.3F),Quaternion.identity);
        
    }

    private void Start()
    {
        if(PhotonNetwork.IsMasterClient)
            CreateController();
    }
}
