using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;
using Random = System.Random;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    public static GameObject controller;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        controller = PhotonNetwork.Instantiate(Path.Combine(Launcher.perso), new Vector2(new Random().Next(-45,-40),-36), Quaternion.identity,0,new object[] {PV.ViewID});
    }
    
    public void Respawn()
    {
        PhotonNetwork.Destroy(controller);
        CreateController();
    }
}
