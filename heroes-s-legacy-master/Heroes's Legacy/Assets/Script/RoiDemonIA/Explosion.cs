using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class Explosion : MonoBehaviour
{
    public GameObject explosion;
    public bool spawnLaunch = false;
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

    }

    // Update is called once per frame
    void Update()
    {
        if(!spawnLaunch)
        {
            StartCoroutine(SpawnBoss());
        }
    }

    [PunRPC]
    void Destroy(int viewid)
    {
        PhotonView todestroy = PhotonView.Find(viewid);
        PhotonNetwork.Destroy(todestroy);

    }


    private IEnumerator SpawnBoss()
    {
        spawnLaunch = true;
        yield return new WaitForSeconds(0.4f);
        PhotonNetwork.Instantiate("roi_demon_phase_2", new Vector3(explosion.transform.position.x, explosion.transform.position.y, 1), explosion.transform.rotation, 0);
        PhotonNetwork.Destroy(explosion);

    }
}
