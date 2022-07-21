using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMana : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxMana = 100;
    public int currentMana;
    public Rigidbody2D rb2d;
    private PhotonView PV;
    private bool isGainingMana = false;

    public ManaBar manaBar;


    void Start()
    {
        currentMana = maxMana;
        manaBar.SetMaxMana(maxMana);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentMana <= 95 && !isGainingMana)
        {
           StartCoroutine(RecoverMana());
        }
    }

    public void LostMana(int manaLoses)
    {
        currentMana -= manaLoses;
        manaBar.SetMana(currentMana);
    }

    public IEnumerator RecoverMana()
    {
        isGainingMana = true;
        yield return new WaitForSeconds(5f);
        currentMana += 5;
        manaBar.SetMana(currentMana);
        isGainingMana = false;
    }

    public int GetCurrentMana()
    {
        return currentMana;
    }
}
