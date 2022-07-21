using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isattacking = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Slime" || collision.tag == "Faucheuse" || collision.tag == "BossFinal")
        {
            if (!isattacking)
            {
                isattacking = true;
                StartCoroutine(MakeDamage(collision));
            }
            
        }
    }

    private IEnumerator MakeDamage(Collider2D collision)
    {
        if (collision.name == "slime_01(Clone)")
        {
            SlimeIA slimeIA = collision.transform.GetComponent<SlimeIA>();
            slimeIA.SlimeDamage(5);
            Waiting();
            yield return new WaitForSeconds(1f);
        }
        else if (collision.name == "slime_02(Clone)")
        {
            Slime_IA_02 slimeIA = collision.transform.GetComponent<Slime_IA_02>();
            slimeIA.SlimeDamage(5);
            Waiting();
            yield return new WaitForSeconds(1f);
        }
        else if (collision.name == "slime_03(Clone)")
        {
            Slime_IA_03 slimeIA = collision.transform.GetComponent<Slime_IA_03>();
            slimeIA.SlimeDamage(5);
            Waiting();
            
            yield return new WaitForSeconds(2f);
        }
        else if (collision.name == "slime_04(Clone)")
        {
            Slime_IA_04 slimeIA = collision.transform.GetComponent<Slime_IA_04>();
            slimeIA.SlimeDamage(5);
            Waiting();
            yield return new WaitForSeconds(1f);
        }
        else if (collision.name == "slimeboss")
        {
            slimebossIA bossIA = collision.transform.GetComponent<slimebossIA>();
            bossIA.SlimeDamage(5);
            Debug.Log("Degat envoyé");
            Waiting();
            yield return new WaitForSeconds(1f);
        }
        else if (collision.tag == "Faucheuse")
        {
            if (collision.name == "bossfaucheuse")
            {
                bossfaucheuseIA fauIA = collision.transform.GetComponent<bossfaucheuseIA>();
                fauIA.bossFaucheuseDamage(5);
                Waiting();
                yield return new WaitForSeconds(1f);
            }
            else
            {
                faucheuseIA fauIA = collision.transform.GetComponent<faucheuseIA>();
                fauIA.FaucheuseDamage(5);
                Waiting();
                yield return new WaitForSeconds(1f);
            }
        }
        else if (collision.tag == "BossFinal")
        {
            if (collision.gameObject.name == "roi_demon_phase_2(Clone)")
            {
                roiphase2IA demonIA = collision.transform.GetComponent<roiphase2IA>();
                demonIA.RoiDamage(5);
                Waiting();
                yield return new WaitForSeconds(1f);

            }
            else
            {
                roidemonIA demonIA = collision.transform.GetComponent<roidemonIA>();
                demonIA.RoiDamage(5);
                Waiting();
                yield return new WaitForSeconds(1f);

            }
        }

    }

    public void Waiting()
    {
        isattacking = false;
    }


}
