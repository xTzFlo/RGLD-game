using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float speed;
    public Transform[] waypoints; //tableau de points qui définissent le chemin


    private Transform target;
    private int destPoint = 0; //index pour waypoints

    // Start is called before the first frame update
    void Start()
    {
        target = waypoints[0];
    }

    private void Awake()
    {
        if (transform.position.x == -31 && transform.position.y == -37)
        {
            waypoints[0] = GameObject.Find("WaypointPlatform1").transform;
            waypoints[1] = GameObject.Find("WaypointPlatform2").transform;
        }
        if (transform.position.x == -15 && transform.position.y == -38)
        {
            waypoints[0] = GameObject.Find("WaypointPlatform1_2").transform;
            waypoints[1] = GameObject.Find("WaypointPlatform2_2").transform;
        }
        if (transform.position.x == -5.52F && transform.position.y == -36)
        {
            waypoints[0] = GameObject.Find("WaypointPlatform1_3").transform;
            waypoints[1] = GameObject.Find("WaypointPlatform2_3").transform;
        }
        if (transform.position.x == 3 && transform.position.y == -37.36F)
        {
            waypoints[0] = GameObject.Find("WaypointPlatform1_4").transform;
            waypoints[1] = GameObject.Find("WaypointPlatform2_4").transform;
        }
        if (transform.position.x == 10 && transform.position.y == -37.3F)
        {
            waypoints[0] = GameObject.Find("WaypointPlatform1_5").transform;
            waypoints[1] = GameObject.Find("WaypointPlatform2_5").transform;
        }
        if (transform.position.x == 17 && transform.position.y == -37.3F)
        {
            waypoints[0] = GameObject.Find("WaypointPlatform1_6").transform;
            waypoints[1] = GameObject.Find("WaypointPlatform2_6").transform;
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