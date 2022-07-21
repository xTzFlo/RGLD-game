using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class roiphase2IA : MonoBehaviour
{
    // Start is called before the first frame update

    public int bossHealth;

    public GameObject roiDemon;

    public SpriteRenderer graphics;

    public Animator animBoss;

    public GameObject Portal;

    public Transform sortposition;
    
    public GameObject Win_Windows;

    public bool isTime = false;

    PhotonView PV;
    int id;



    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        id = GetComponent<PhotonView>().ViewID;
        Win_Windows = roidemonIA.instance.Win_Windows;

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
        if(!isTime)
        {
            StartCoroutine(launchSort());
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

        if (bossHealth <= 0)
        {
            PV.RPC("Destroy", RpcTarget.MasterClient, id);
        }
    }


    [PunRPC]
    void Destroy(int viewid)
    {
        PV.RPC("spawn",RpcTarget.All);
        PhotonView todestroy = PhotonView.Find(viewid);
        PhotonNetwork.Destroy(todestroy);

    }
    
    [PunRPC] void spawn()
    {
        Win_Windows.SetActive(true);
    }

    private IEnumerator GraphicEffect()
    {
        graphics.color = new Color(1f, 0.5f, 0.5f, 1f);
        yield return new WaitForSeconds(0.3f);
        graphics.color = new Color(1f, 1f, 1f, 1f);
    }

    public IEnumerator launchSort()
    {
        isTime = true;
        yield return new WaitForSeconds(10f);
        animBoss.SetTrigger("sort");
        yield return new WaitForSeconds(0.5f);
        roiphase2 animScript = animBoss.GetBehaviour<roiphase2>();
        bool isFlipped = animScript.isFlipped;

        if(isFlipped)
        {
            PhotonNetwork.Instantiate("demonSort", new Vector3(sortposition.position.x, sortposition.position.y, 0), sortposition.rotation, 0);
        }
        else
        {
            PhotonNetwork.Instantiate("demonSort", new Vector3(sortposition.position.x, sortposition.position.y, 1), sortposition.rotation, 0);
        }

        if(bossHealth < 50)
        {
            yield return new WaitForSeconds(1f);
            animBoss.SetTrigger("sort");
            yield return new WaitForSeconds(0.5f);
            isFlipped = animScript.isFlipped;
            if (isFlipped)
            {
                PhotonNetwork.Instantiate("demonSort", new Vector3(sortposition.position.x, sortposition.position.y, 0), sortposition.rotation, 0);
            }
            else
            {
                PhotonNetwork.Instantiate("demonSort", new Vector3(sortposition.position.x, sortposition.position.y, 1), sortposition.rotation, 0);
            }
        }

        if(bossHealth < 25)
        {
            yield return new WaitForSeconds(1f);
            animBoss.SetTrigger("sort");
            yield return new WaitForSeconds(0.5f);
            isFlipped = animScript.isFlipped;

            if (isFlipped)
            {
                PhotonNetwork.Instantiate("demonSort", new Vector3(sortposition.position.x, sortposition.position.y, 0), sortposition.rotation, 0);
            }
            else
            {
                PhotonNetwork.Instantiate("demonSort", new Vector3(sortposition.position.x, sortposition.position.y, 1), sortposition.rotation, 0);
            }
        }
        isTime = false;

    }


}
