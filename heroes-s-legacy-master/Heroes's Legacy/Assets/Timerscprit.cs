using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timerscprit : MonoBehaviour
{
    [SerializeField] TMP_Text TIMER;
    private void Awake()
    {
        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        printTime();
    }

    void printTime()
    {
        TIMER.text = ((int) Time.time).ToString();
    }
}
