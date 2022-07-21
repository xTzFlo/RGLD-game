using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class IABossSlime2 : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints; //tableau de points qui définissent le chemin

    public int SlimeHealth = 80;

    public GameObject slime;

    public SpriteRenderer graphics;

    private Transform target;
    private int destPoint = 0; //index pour waypoints

    PhotonView PV;
    int id;

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    // Update is called once per frame
    void Update()
    {
        // Système de déplacement
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);    //on normalise la direction (à 1)


        //si on atteint un point, on se dirige vers le suivant
        if (Vector3.Distance(transform.position, target.position) < 0.3f)
        {
            destPoint = (destPoint + 1) % waypoints.Length;
            target = waypoints[destPoint];
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Chevalier") || collision.transform.CompareTag("Mage"))
        {
            PlayerHealth playerHealth = collision.transform.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(20);
            StartCoroutine(stop());
        }
    }
    private IEnumerator stop()
    {   
		target = null;
		yield return new WaitForSeconds(4f);
		target = waypoints[0];
		yield return new WaitForSeconds(4f);
    }

    public void SlimeDamage(int damage)
    {
        SlimeHealth -= damage;
		Debug.Log("touche");
        StartCoroutine(GraphicEffect());
        
        if (SlimeHealth <= 0)
        {
            Debug.Log("SlimeDEAD");
            Debug.Log(id);
            PV.RPC("Destroy", RpcTarget.MasterClient, id);
        }
    }

    [PunRPC]  void Destroy(int viewid)
    {
        PhotonView todestroy = PhotonView.Find(viewid);
        PhotonNetwork.Destroy(todestroy);

    }    

    private IEnumerator GraphicEffect()
    {
        graphics.color = new Color(1f,0.5f,0.5f,1f);
        yield return new WaitForSeconds(0.3f);
        graphics.color = new Color(1f,1f,1f,1f);
    }
    
}
