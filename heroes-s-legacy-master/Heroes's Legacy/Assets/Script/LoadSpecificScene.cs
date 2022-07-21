using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class LoadSpecificScene : MonoBehaviour
{
    
    [SerializeField] GameObject win_windows;
    protected List<Collider2D> list = new List<Collider2D>();
    public Animator transitionAnim;
    protected bool stop = false;
    private PhotonView PV;
    public GameObject Panel;
    public static LoadSpecificScene instance;
    public bool OnBoss = false;

    private void Awake()
    {
        PV =  GetComponent<PhotonView>();
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "projectile" && collision.tag != "Arme")
        {
            list.Add(collision);
            Panel.SetActive(true);
        }
    }
    
    

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "projectile")
        {
            list.Remove(other);
            win_windows.SetActive(false);
        }
    }

    private void Update()
    {
        if (stop)
            return;
        
        if (PhotonNetwork.PlayerList.Length == list.Count)
        {
            win_windows.SetActive(true);
            if (RoomManager.Instance.On6)
            {
                RoomManager.Instance.printTime();
            }
        }
        else
        {
            win_windows.SetActive(false);
            if (RoomManager.Instance.On6)
            {
                Debug.Log("Time doit s'afficher");
                RoomManager.Instance.printTime();
            }
        }
        
    }

    IEnumerator LoadScene_1()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        PhotonNetwork.LoadLevel(2);
        OnBoss = true;
    }

    IEnumerator LoadScene_02()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        PhotonNetwork.LoadLevel(3);
        OnBoss = false;
    }

    IEnumerator LoadScene_03()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5f);
        PhotonNetwork.LoadLevel(4);
        OnBoss = true;
    }
    
    
    IEnumerator Load_Scene_04()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5F);
        PhotonNetwork.LoadLevel(5);
        OnBoss = false;
    }

    IEnumerator LoadScene_Deamon()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1.5F);
        PhotonNetwork.LoadLevel(6);
    }

    
    
    
    public void Next_Level()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            stop = true;
            win_windows.SetActive(false);
            StartCoroutine(LoadScene_1());
        }
        else
        {
            PV.RPC("Next", RpcTarget.MasterClient);
        }
        
    }
    public void To_Lvl_2()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            stop = true;
            win_windows.SetActive(false);
            StartCoroutine(LoadScene_02());
        }
        else
        {
            PV.RPC("Next_2", RpcTarget.MasterClient);
        }
       
    }

    public void To_Lvl_3()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            stop = true;
            win_windows.SetActive(false);
            StartCoroutine(LoadScene_03());
        }
        else
        {
            PV.RPC("Next_3", RpcTarget.MasterClient);
        }
    }

    public void To_Lvl_4()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            stop = true;
            win_windows.SetActive(false);
            StartCoroutine(Load_Scene_04());
        }
        else
        {
            PV.RPC("Next_4", RpcTarget.MasterClient);
        }
    }
    
    public void To_Deamon_Boss()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            stop = true;
            win_windows.SetActive(false);
            StartCoroutine(LoadScene_Deamon());
        }
        else
        {
            PV.RPC("To_Deamon", RpcTarget.MasterClient);
        }
    }
    
    [PunRPC]  void Next()
    {
        stop = true;
        win_windows.SetActive(false);
        StartCoroutine(LoadScene_1());
    }
    
    [PunRPC]  void Next_2()
    {
        stop = true;
        win_windows.SetActive(false);
        StartCoroutine(LoadScene_02());
    }
    
    [PunRPC] void Next_3()
    {
        stop = true;
        win_windows.SetActive(false);
        StartCoroutine(LoadScene_03());
    }
    [PunRPC] void Next_4()
    {
        stop = true;
        win_windows.SetActive(false);
        StartCoroutine(Load_Scene_04());
    }

    [PunRPC] void To_Deamon()
    {
        stop = true;
        win_windows.SetActive(false);
        StartCoroutine(LoadScene_Deamon());
    }
}
