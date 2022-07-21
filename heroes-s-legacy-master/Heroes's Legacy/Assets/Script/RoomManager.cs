using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using TMPro;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;
    public GameObject TIMER;
    public GameObject Win;
    public  int Time_to_Begin;
    public bool On6 = false;

    private void Awake()
    {
        if (Instance) // checks if another RoomManager exists
        {
            Destroy(gameObject); // there can only be one 
            return;
        }
        DontDestroyOnLoad(gameObject); // i am the only one...
        Instance = this;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode loadSceneMode)
    {
        if (scene.buildIndex == 1)
        {
            PhotonNetwork.Instantiate(Path.Combine("PlayerManager"), Vector2.zero,
                Quaternion.identity);
            if(PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate(Path.Combine("Enemy_manager_lvl1"), Vector2.zero,
                Quaternion.identity);
            TIMER = GameObject.Find("Timer");
            Time_to_Begin = (int) Time.time;
            On6 = false;
        }
        else if (scene.buildIndex == 2)
        {
            PhotonNetwork.Instantiate(Path.Combine("PlayerManager"), Vector2.zero,
                Quaternion.identity);
            TIMER = GameObject.Find("Timer");
        }
        else if (scene.buildIndex == 3)
        {
            PhotonNetwork.Instantiate(Path.Combine("PlayerManager"), Vector2.zero,
                Quaternion.identity);
            if (PhotonNetwork.IsMasterClient)
                PhotonNetwork.Instantiate(Path.Combine("Enemy_manager_lvl2"), Vector2.zero,
                Quaternion.identity);
            TIMER = GameObject.Find("Timer");
        }
        else if (scene.buildIndex == 4)
        {
            PhotonNetwork.Instantiate(Path.Combine("PlayerManager"), Vector2.zero,
                Quaternion.identity);
            TIMER = GameObject.Find("Timer");
        }
        else if (scene.buildIndex == 5)
        {
            PhotonNetwork.Instantiate(Path.Combine("PlayerManager"), Vector2.zero,
                Quaternion.identity);
            TIMER = GameObject.Find("Timer");
        }
        else if (scene.buildIndex == 6)
        {
            PhotonNetwork.Instantiate(Path.Combine("PlayerManager"), Vector2.zero,
                Quaternion.identity);
            TIMER = roidemonIA.instance.TIMER;
            Win = roidemonIA.instance.Win_Windows;
            On6 = true;

        }
    }

    private void Update()
    {
        if(!On6)
            return;
        
        if (Win.activeSelf)
            return;
        printTime();
    }

    public void printTime()
    {
        TIMER.GetComponent<TMP_Text>().text = ((int) Time.time - Time_to_Begin).ToString();
    }

    
}
