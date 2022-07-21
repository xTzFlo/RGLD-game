using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class bossfaucheuseIA : MonoBehaviour
{
    // Start is called before the first frame update

    public int bossHealth;

    public GameObject bossFaucheuse;

    public SpriteRenderer graphics;

    public Animator animBoss;

    public GameObject Portal;

    public bool isEnraged = false;

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
        if(isEnraged)
        {
            StartCoroutine(DiscreteEffectAttack());
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

    public void bossFaucheuseDamage(int damage)
    {

        bossHealth -= damage;
        StartCoroutine(GraphicEffect());

        if (bossHealth <= 40 && !isEnraged)
        {
            isEnraged = true;
            //Portal.SetActive(true);
        }

        if (bossHealth <= 0)
        {
            PV.RPC("Destroy", RpcTarget.MasterClient, id);
        }
    }


    [PunRPC]
    void Destroy(int viewid)
    {
        PhotonView todestroy = PhotonView.Find(viewid);
        PhotonNetwork.Destroy(todestroy);
        PV.RPC("Spawn",RpcTarget.All);
    }
    
    [PunRPC] void Spawn()
    {
        Portal.SetActive(true);
    }

    private IEnumerator GraphicEffect()
    {
        graphics.color = new Color(1f, 0.5f, 0.5f, 1f);
        yield return new WaitForSeconds(0.3f);
        graphics.color = new Color(1f, 1f, 1f, 1f);
    }

    private IEnumerator DiscreteEffectAttack()
    {
        isEnraged = false;
        graphics.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.3f);
        graphics.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.3f);

        graphics.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.3f);
        graphics.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.3f);

        graphics.color = new Color(1f, 1f, 1f, 0.5f);
        yield return new WaitForSeconds(0.3f);
        graphics.color = new Color(1f, 1f, 1f, 1f);
        yield return new WaitForSeconds(0.3f);

        graphics.color = new Color(1f, 1f, 1f, 0.5f);
        animBoss.GetBehaviour<bossfaucheuseAnim>().speed += 2f; ;
        yield return new WaitForSeconds(1.5f);
        graphics.color = new Color(1f, 1f, 1f, 1f);
        animBoss.GetBehaviour<bossfaucheuseAnim>().speed -= 2f; ;
        yield return new WaitForSeconds(10f);
        isEnraged = true;




    }
}

