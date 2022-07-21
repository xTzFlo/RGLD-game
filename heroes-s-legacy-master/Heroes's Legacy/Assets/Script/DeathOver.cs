using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using TMPro;

public class DeathOver : MonoBehaviour
{
    private float time;
    [SerializeField] TMP_Text Timer_text;
    public GameObject gameOverUI;
    public float time_to_begin;
    [SerializeField] GameObject RespawnButton;
    [SerializeField] GameObject Timer;
    PhotonView PV;
    PlayerManager playerManager;
    public static DeathOver instance;
    
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        if(PV.IsMine)
            instance = this;
    }
    

    public void OnPlayerDeath()
    {
        gameOverUI.SetActive(true);
        time_to_begin = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if (!PV.IsMine)
            return;
        
        if (!gameOverUI.activeSelf)
            return;
       
        time = (int) (15 - (Time.time - time_to_begin));
        Timer_text.text = time.ToString();

        if (time <= 0)
        {
            RespawnButton.SetActive(true);
            Timer.SetActive(false);
            time = 0;
        }
    }

    public void Respawn()
    {
        gameOverUI.SetActive(false);
        PlayerHealth.instance.Respawn();
    }
}
