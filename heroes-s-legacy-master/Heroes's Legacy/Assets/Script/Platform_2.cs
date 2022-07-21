using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Platform_2 : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints; //tableau de points qui définissent le chemin


    private Transform target;
    private int destPoint = 0; //index pour waypoints

    // Start is called before the first frame update
    void Start()
    {
        //target = waypoints[0];
    }

    private void Awake()
    {
        if (transform.position.x == -21.25F && transform.position.y == -36.30F)
        {
            waypoints = new Transform[9];
            waypoints[0] = GameObject.Find("WaypointPlatform1").transform;
            waypoints[1] = GameObject.Find("WaypointPlatform2").transform;
            waypoints[2] = GameObject.Find("WaypointPlatform3").transform;
            waypoints[3] = GameObject.Find("WaypointPlatform4").transform;
            waypoints[4] = GameObject.Find("WaypointPlatform5").transform;
            waypoints[5] = GameObject.Find("WaypointPlatform4").transform;
            waypoints[6] = GameObject.Find("WaypointPlatform3").transform;
            waypoints[7] = GameObject.Find("WaypointPlatform2").transform;
            waypoints[8] = GameObject.Find("WaypointPlatform1").transform;
            target = waypoints[0];
        }
        if (transform.position.x == -7 && transform.position.y == -36.39F)
        {
            waypoints = new Transform[2];
            waypoints[0] = GameObject.Find("WaypointPlatform1_2").transform;
            waypoints[1] = GameObject.Find("WaypointPlatform2_2").transform;
            /*waypoints[2] = GameObject.Find("WaypointPlatform3_2").transform;
            waypoints[3] = GameObject.Find("WaypointPlatform4_2").transform;
            waypoints[4] = GameObject.Find("WaypointPlatform5_2").transform;
            waypoints[5] = GameObject.Find("WaypointPlatform4_2").transform;
            waypoints[6] = GameObject.Find("WaypointPlatform3_2").transform;
            waypoints[7] = GameObject.Find("WaypointPlatform2_2").transform;
            waypoints[8] = GameObject.Find("WaypointPlatform1_2").transform;*/
            target = waypoints[0];

        }
        if (transform.position.x == 5.49F && transform.position.y == -36.39F)
        {
            waypoints = new Transform[2];
            waypoints[0] = GameObject.Find("WaypointPlatform1_3").transform;
            waypoints[1] = GameObject.Find("WaypointPlatform2_3").transform;
            /*waypoints[2] = GameObject.Find("WaypointPlatform3_3").transform;
            waypoints[3] = GameObject.Find("WaypointPlatform4_3").transform;
            waypoints[4] = GameObject.Find("WaypointPlatform5_3").transform;
            waypoints[5] = GameObject.Find("WaypointPlatform4_3").transform;
            waypoints[6] = GameObject.Find("WaypointPlatform3_3").transform;
            waypoints[7] = GameObject.Find("WaypointPlatform2_3").transform;
            waypoints[8] = GameObject.Find("WaypointPlatform1_3").transform;*/
            target = waypoints[0];

        }
        
        
        
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
    
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag != "projectile")
        {
            other.transform.parent = this.transform;

        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag != "projectile")
        {
            other.transform.parent = null;
        }
        
    }
}
