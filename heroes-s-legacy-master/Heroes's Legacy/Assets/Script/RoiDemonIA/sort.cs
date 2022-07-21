using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class sort : MonoBehaviour
{
    public float sortspeed;

    private Rigidbody2D rgdb;

    public GameObject demonSort;

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
        if (rgdb.transform.localPosition.z >= 1)
        {
            rgdb.transform.localScale = new Vector3(0.3f, -0.3f, 0.3f);
            rgdb.velocity = transform.up * sortspeed;

        }
        else
        {
            rgdb.velocity = -transform.up * sortspeed;
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


        if (collision.tag == "CHevalier" || collision.tag == "Mage")
        {
            
            StartCoroutine(MakeDamage(collision));
            

        }

    }


    private IEnumerator MakeDamage(Collider2D collision)
    {
        PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(20);
        PV.RPC("Destroy", RpcTarget.MasterClient, id);
        yield return new WaitForSeconds(2f);



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
        PhotonNetwork.Destroy(demonSort);
    }
}
