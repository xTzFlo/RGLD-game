using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class roidemonIA : MonoBehaviour
{
    // Start is called before the first frame update

    public int bossHealth;

    public GameObject roiDemon;

    public SpriteRenderer graphics;

    public Animator animBoss;

    public GameObject Win_Windows;
    public GameObject TIMER;

    public bool isIdle = false;
    PhotonView PV;

    public static roidemonIA instance;
    int id;



    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        id = GetComponent<PhotonView>().ViewID;
        instance = this;
    }

    public bool IsMine()
    {
        bool str = PV.IsMine;
        return str;
    }

    void Start()
    {
        animBoss = GetComponent<Animator>();
    }

    void Update()
    {
        if(!isIdle)
        {
            StartCoroutine(LetRestPlayer());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Chevalier") || collision.transform.CompareTag("Mage"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(20);
        }
    }

    public void RoiDamage(int damage)
    {
        
        bossHealth -= damage;
        StartCoroutine(GraphicEffect());
        
        if (bossHealth <= 75)
        { 
            StartCoroutine(BossEnraged());

            //Portal.SetActive(true);
        }
    }


    [PunRPC]
    void Destroy(int viewid)
    {
        PhotonView todestroy = PhotonView.Find(viewid);
        PhotonNetwork.Destroy(todestroy);

    }

    private IEnumerator GraphicEffect()
    {
        graphics.color = new Color(1f, 0.5f, 0.5f, 1f);
        yield return new WaitForSeconds(0.3f);
        graphics.color = new Color(1f, 1f, 1f, 1f);
    }

    private IEnumerator BossEnraged()
    {
        //Faire spawn explosion, puis phase 2
        PhotonNetwork.Instantiate("Explosion", new Vector3(roiDemon.transform.position.x, roiDemon.transform.position.y, 1), roiDemon.transform.rotation, 0);
        PV.RPC("Destroy", RpcTarget.MasterClient, id);
        yield return new WaitForSeconds(0.3f);



    }

    private IEnumerator LetRestPlayer()
    {
        isIdle = true;
        yield return new WaitForSeconds(8f);
        yield return new WaitForSeconds(2f);
        isIdle = false;
    }

}

