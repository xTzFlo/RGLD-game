﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Controller : MonoBehaviour
{
    public Rigidbody2D rb2d;    //créer une variable de type de rigidbody
    public BoxCollider2D playercolider;

    public SpriteRenderer spriterenderer;

    public float speed;     //créer une variable vitesse, à définir directement sur Unity
    public float jumpForce;     //créer une variable de force du saut, à définir directement sur Unity

    public int maxJumps = 2;     //créer une variable de sauts maximum, définie à 2
    public int jumps;     //variable pour savoir combien de jumps le joueur peut encore faire

    public bool isgrouded = true;     //booléen pour savoir si le joueur touche le sol ou non
    public bool isrunning = false;     //booléen pour savoir si le joueur court ou non

    public Transform groundcheckright;     //deux objets utilisés pour savoir si le joueur est sur le sol ou non (situés sous ses pieds)
    public Transform groundcheckleft;

    public Animator animChevalier;

    public bool facingleft = false;     //booléen pour savoir si le joueur fait face à gauche ou non

    public bool canAttack = true;
    public GameObject weapon;
    public GameObject ui;


    private Vector3 velocity = Vector3.zero;

    PhotonView PV;
    public static Controller instance;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (PV.IsMine)
            instance = this;
    }

    public bool IsMine()
    {
        bool str = PV.IsMine;
        return str;
    }
    

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        animChevalier = GetComponent<Animator>();
        if (!PV.IsMine)
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            Destroy(ui);
            //destroy les autres canvas
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!PV.IsMine)
            return;

        float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        
        if (move < 0 && !facingleft)
        {
            facingleft = true;
        }
        else if (move > 0 && facingleft)
        {
            facingleft = false;
        }

        float charactVelocity = Mathf.Abs(rb2d.velocity.x);
        animChevalier.SetFloat("speed", charactVelocity);

        FacingLeft();
        Move(move);

    }

    private void Update()
    {
        if (!PV.IsMine)
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isrunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isrunning = false;
        }
        if (isrunning)
        {
            speed = 140f;
        }
        else
        {
            speed = 65f;
        }
        if(Input.GetAxis("Horizontal") == 0)
        {
            speed = 0f;
        }
        else if (!isrunning)
        {
            speed = 65f;
        }
    


        if (Input.GetButtonDown("Jump"))
        {
            this.Jump();
        }
        //float move = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        
        if (Input.GetKey(KeyCode.Mouse0))
        {
            animChevalier.SetBool("attack", true);
            StartCoroutine(Attack());
        }

        //Move(move);
    }

    void Move(float _move)
    {
        Vector3 target = new Vector2(_move, rb2d.velocity.y);
        rb2d.velocity = Vector3.SmoothDamp(rb2d.velocity, target, ref velocity, .05f);
        
    }

    void FacingLeft()
    {
        if (facingleft && rb2d.transform.localScale.x > 0)
        {
            rb2d.transform.localScale = new Vector3(-0.05f, 0.05f, 0.05f);
        }
        else if (!facingleft && rb2d.transform.localScale.x < 0)
        {
            rb2d.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        }
    }

   void Jump()
    {
        if (jumps > 0)
        {
            rb2d.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            jumps = jumps - 1;
        }
        else
        {
            return;
        }
    }

    
    void OnCollisionEnter2D(Collision2D collider)
    {
        jumps = maxJumps;
    }

    public IEnumerator Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            weapon.SetActive(true); 
            yield return new WaitForSeconds(0.5f);
            weapon.SetActive(false);
            animChevalier.SetBool("attack", false);
            canAttack = true;
             
        }
       



        //code : lance l'animation de l'attaque
        //dans de l'ia, la boite de collision de l'arme le touche, il prendra des dégats
    }
}


