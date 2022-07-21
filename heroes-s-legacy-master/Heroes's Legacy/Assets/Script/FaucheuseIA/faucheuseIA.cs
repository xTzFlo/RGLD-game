using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class faucheuseIA : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints; //tableau de points qui définissent le chemin

    public int FaucheuseHealth = 20;

    public GameObject faucheuse;

    public SpriteRenderer graphics;
    
    private Transform target;
    private int destPoint = 0; //index pour waypoints

    public bool isFlipped = false;

    PhotonView PV;
    int id;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        id = GetComponent<PhotonView>().ViewID;

        //if(PV.IsMine )

        /*waypoints[0] = GameObject.Find("Waypoint1_1").transform;
        if (waypoints[0] == null)
            Debug.Log("marche pas waipoints 0");
        waypoints[1] = GameObject.Find("Waypoint1_2").transform;
        if (waypoints[1] == null)
            Debug.Log("marche pas waipoints 1");*/
    }

    public bool IsMine()
    {
        bool str = PV.IsMine;
        return str;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (waypoints.Length != 0)
        {
            target = waypoints[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints.Length == 0)
        {
           return; 
        }
        // Système de déplacement
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);    //on normalise la direction (à 1)


        //si on atteint un point, on se dirige vers le suivant
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
            isFlipped = !isFlipped;
            if(isFlipped)
            {
                graphics.flipX = true;
            }
            else
            {
                graphics.flipX = false;

            }
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

    public void FaucheuseDamage(int damage)
    {
        FaucheuseHealth -= damage;
        StartCoroutine(GraphicEffect());

        if (FaucheuseHealth <= 0)
        {
            PV.RPC("Destroy", RpcTarget.MasterClient, id);
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
}
