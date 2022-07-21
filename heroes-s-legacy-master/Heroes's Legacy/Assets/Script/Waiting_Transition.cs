using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waiting_Transition : MonoBehaviour
{
    private float time;
    [SerializeField] private GameObject Transition;

    private void Start()
    {
        time = Time.time;
    }

    private void Update()
    {
        if (Time.time >= time + 2)
        {
            Transition.SetActive(false);
        }
    }
}
