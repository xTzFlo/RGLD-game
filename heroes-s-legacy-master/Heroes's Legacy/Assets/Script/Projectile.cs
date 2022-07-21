using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Projectile : MonoBehaviour
{
    public float projectilespeed;

    public bool isattacking = false;
    private Rigidbody2D rgdb;

    public GameObject flame;

    public Animator animFlame;

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

    // Start is called before the first frame update
    void Start()
    {
        rgdb = GetComponent<Rigidbody2D>();
        if(rgdb.transform.localPosition.z >= 1)
        {
            Debug.Log("FACINGLEFT");
            rgdb.transform.localScale = new Vector3(0.3f, -0.3f, 0.3f); 
            rgdb.velocity = transform.up * projectilespeed;

        }
        else
        {
            rgdb.velocity = -transform.up * projectilespeed;
        }
        animFlame = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
      StartCoroutine(TimeEnd());

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        animFlame.SetBool("meetObs", true);


        if (collision.tag == "Slime" || collision.tag == "Faucheuse" || collision.tag == "BossFinal")
        {
            if (!isattacking)
            {
                isattacking = true;
                StartCoroutine(MakeDamage(collision));
            }
         
        }

    }


    private IEnumerator MakeDamage(Collider2D collision)
    {
        if (collision.name == "slime_01(Clone)")
        {
            SlimeIA slimeIA = collision.transform.GetComponent<SlimeIA>();
            slimeIA.SlimeDamage(5);
            Waiting();
            PV.RPC("Destroy", RpcTarget.MasterClient, id);
            yield return new WaitForSeconds(2f);
        }
        else if (collision.name == "slime_02(Clone)")
        {
            Slime_IA_02 slimeIA = collision.transform.GetComponent<Slime_IA_02>();
            slimeIA.SlimeDamage(5);
            Waiting();
            PV.RPC("Destroy", RpcTarget.MasterClient, id);
            yield return new WaitForSeconds(2f);
        }
        else if (collision.name == "slime_03(Clone)")
        {
            Slime_IA_03 slimeIA = collision.transform.GetComponent<Slime_IA_03>();
            slimeIA.SlimeDamage(5);
            Waiting();
            PV.RPC("Destroy", RpcTarget.MasterClient, id);
            yield return new WaitForSeconds(2f);
        }
        else if (collision.name == "slime_04(Clone)")
        {
            Slime_IA_04 slimeIA = collision.transform.GetComponent<Slime_IA_04>();
            slimeIA.SlimeDamage(5);
            Waiting();
            PV.RPC("Destroy", RpcTarget.MasterClient, id);
            yield return new WaitForSeconds(2f);
        }
        else if (collision.gameObject.name == "slimeboss")
        {
            slimebossIA slimeIA = collision.transform.GetComponent<slimebossIA>();
            slimeIA.SlimeDamage(5);
            Waiting();
            PV.RPC("Destroy", RpcTarget.MasterClient, id);
            yield return new WaitForSeconds(1f);
        }
        else if (collision.tag == "Faucheuse")
        {
            if(collision.name == "bossfaucheuse")
            {
                bossfaucheuseIA fauIA = collision.transform.GetComponent<bossfaucheuseIA>();
                fauIA.bossFaucheuseDamage(5);
                Waiting();
                PV.RPC("Destroy", RpcTarget.MasterClient, id);
                yield return new WaitForSeconds(1f);
            }
            else
            {
                faucheuseIA fauIA = collision.transform.GetComponent<faucheuseIA>();
                fauIA.FaucheuseDamage(5);
                Waiting();
                PV.RPC("Destroy", RpcTarget.MasterClient, id);
                yield return new WaitForSeconds(1f);
            }
            
        }
        else if (collision.tag == "BossFinal")
        {
            if(collision.gameObject.name == "roi_demon_phase_2(Clone)")
            {
                roiphase2IA demonIA = collision.transform.GetComponent<roiphase2IA>();
                demonIA.RoiDamage(5);
                Waiting();
                PV.RPC("Destroy", RpcTarget.MasterClient, id);
                yield return new WaitForSeconds(1f);

            }
            else
            {
                roidemonIA demonIA = collision.transform.GetComponent<roidemonIA>();
                demonIA.RoiDamage(5);
                Waiting();
                PV.RPC("Destroy", RpcTarget.MasterClient, id);
                yield return new WaitForSeconds(1f);

            }
            
        }

    }

    public void Waiting()
    {
        isattacking = false;
    }

    [PunRPC]
    void Destroy(int viewid)
    {
        PhotonView todestroy = PhotonView.Find(viewid);
        PhotonNetwork.Destroy(todestroy);

    }

    private IEnumerator TimeEnd()
    {
        yield return new WaitForSeconds(2f);
        animFlame.SetBool("timeEnd", true);
        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.Destroy(flame);
    }
}
