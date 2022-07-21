using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    // booleen pour activer l'animation de marche
    public float speed = 1f;
    // représente l'animator du chevalier
    Animator animC;
    // rigidbody du joueur
    Rigidbody2D rigidplayer;
    // pour gérer si le joueur va à gauche ou à droite
    bool facingLeft = false;

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }


    void Start()
    {
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
        rigidplayer = GetComponent<Rigidbody2D>();
        animC = GetComponent<Animator>();
    }

    // Fonction appelée à chaque frame du jeu
    void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        float move = Input.GetAxisRaw("Horizontal");
        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")) * speed * Time.fixedDeltaTime;


        if (movement_vector != Vector2.zero)
        {
            animC.SetBool("marche", true);
        }
        else
        {
            animC.SetBool("marche", false);
        }

        rigidplayer.MovePosition(rigidplayer.position + movement_vector);


        if (move < 0 && !facingLeft)
        {
            facingLeft = !facingLeft;
            animC.SetBool("gauche", true);
        }
        else if (move > 0 && facingLeft)
        {
            facingLeft = !facingLeft;
            animC.SetBool("gauche", false);
        }


    }
}

