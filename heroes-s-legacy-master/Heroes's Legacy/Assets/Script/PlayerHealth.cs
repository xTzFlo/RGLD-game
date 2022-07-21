using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] GameObject settingsWindows;

    public bool isinvincible = false;

    public SpriteRenderer graphics;

    public HealthBar healthBar;
    private PhotonView PV;
    public static PlayerHealth instance;
    private PlayerManager playerManager;
    private RoomManager _roomManager;
    public Rigidbody2D rb2d;
    public GameObject CamCramoisi;
    public bool death = false;
    public bool Active = false;
    
    
    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int) PV.InstantiationData[0]).GetComponent<PlayerManager>();
        _roomManager = RoomManager.Instance;
    }
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        if (PV.IsMine)
            instance = this;

        if (!PV.IsMine)
        {
            Destroy(CamCramoisi);
        }
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeDamage(20);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Active)
            {
                settingsWindows.SetActive(false);
                Active = false;
            }
            else
            {
                settingsWindows.SetActive(true);
                Active = true;
            }
            
        }
    }
    
    public void CloseSettingsWindows()
    {
        settingsWindows.SetActive(false);
    }

    public void leave_game()
    {
        if (Launcher.Instance.SinglePlayer)
        {
            Destroy(_roomManager);
            PhotonNetwork.LeaveRoom();
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene(0);
        }
        else
        {
            Destroy(_roomManager);
            PhotonNetwork.LeaveRoom();
            SceneManager.LoadScene(0);
            
        }
        
    }

    public void TakeDamage(int damage)
    {
        if (!isinvincible && PV.IsMine) 
        {
            currentHealth -= damage;
            healthBar.SetHealth(currentHealth);
            isinvincible = true;
            StartCoroutine(InvincibilityFlash());
            StartCoroutine(EndInvincibility());
        }
        if (currentHealth <= 0 && PV.IsMine)
        {
            Die();
        }
        
    }

    public void HealthCare(int health)
    {
        if (PV.IsMine)
        {
            currentHealth += health;
            if (currentHealth > 100)
                currentHealth = 100;
            healthBar.SetHealth(currentHealth);
        }
    }

    public IEnumerator InvincibilityFlash()
    {
        graphics.color = new Color(1f,0.5f,0.5f,1f);
        yield return new WaitForSeconds(0.3f);
        graphics.color = new Color(1f,1f,1f,1f);
        while (isinvincible)
        {
            graphics.color = new Color(1f,1f,1f,0.5f);
            yield return new WaitForSeconds(0.3f);
            graphics.color = new Color(1f,1f,1f,1f);
            yield return new WaitForSeconds(0.3f);
        }
        graphics.color = new Color(1f,1f,1f,1f);

    }

    public IEnumerator EndInvincibility()
    {
        yield return new WaitForSeconds(3f);
        isinvincible = false;
    }
    
    public void Die()
    {
        if(Launcher.perso == "mage")
        {    
            MageController.instance.enabled = false;
            MageController.instance.rb2d.bodyType = RigidbodyType2D.Kinematic;
            MageController.instance.playercolider.enabled = false;
            MageController.instance.spriterenderer.enabled = false;
        }
        else
        {
            Controller.instance.enabled = false;
            Controller.instance.rb2d.bodyType = RigidbodyType2D.Kinematic;
            Controller.instance.playercolider.enabled = false;
            Controller.instance.spriterenderer.enabled = false;
        }
        rb2d.transform.localScale = new Vector3(0.0000001f, 0.00000001f, 0.05f);
        try
        {
            Destroy(GetComponentInChildren<Camera>().gameObject);
        }
        catch (Exception e)
        {
            Debug.Log("pas de cam");
        }
        death = true;

        DeathOver.instance.OnPlayerDeath();
        
    }
    public void Respawn()
    {
        death = false;
        playerManager.Respawn();
    }
    
}
