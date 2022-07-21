using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class slimebossIA : MonoBehaviour
{
    // Start is called before the first frame update

    public int bossHealth;

    public GameObject slimeBoss;

    public SpriteRenderer graphics;

    public Animator animBoss;

    public GameObject Portal;

    public bool isEnraged = false;
    public bool isInvicible = false;
    PhotonView PV;
    int id;

   

    private void Awake()
    { 
        PV = GetComponent<PhotonView>();
        id = GetComponent<PhotonView>().ViewID;
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
        if (bossHealth < 40 && !isEnraged)
        {
            StartCoroutine(BossEnraged());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Chevalier") || collision.transform.CompareTag("Mage"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            if (isEnraged)
            {
                playerHealth.TakeDamage(25);
            }
            else
            {
                playerHealth.TakeDamage(20);
            }
        }
    }

    public void SlimeDamage(int damage)
    {
        if(!isInvicible)
        {
            bossHealth -= damage;
            StartCoroutine(GraphicEffect());
        }

        
        if (bossHealth <= 0)
        {
           Debug.Log(id);
           PV.RPC("Destroy", RpcTarget.MasterClient, id);
        }
    }

    [PunRPC] void Spawn()
    {
        Portal.SetActive(true);
    }
    

    [PunRPC]  void Destroy(int viewid)
    {
        PhotonView todestroy = PhotonView.Find(viewid);
        PhotonNetwork.Destroy(todestroy);
        PV.RPC("Spawn",RpcTarget.All);
    }

    private IEnumerator GraphicEffect()
    {
        graphics.color = new Color(1f, 0.5f, 0.5f, 1f);
        yield return new WaitForSeconds(0.3f);
        graphics.color = new Color(1f, 1f, 1f, 1f);
    }

    private IEnumerator BossEnraged()
    {
        isEnraged = true;
        isInvicible = true;
        animBoss.SetBool("IsEnraged", true);
        yield return new WaitForSeconds(2f);
        isInvicible = false;
    }
}
