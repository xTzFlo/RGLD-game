using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePanel : MonoBehaviour
{

    public GameObject Panel;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D other)
    {
        Panel.SetActive(false);
    }
}
