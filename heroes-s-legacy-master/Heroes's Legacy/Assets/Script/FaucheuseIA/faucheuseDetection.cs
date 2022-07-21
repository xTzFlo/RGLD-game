using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class faucheuseDetection : StateMachineBehaviour
{
    GameObject[] mages;
    GameObject[] chevaliers;
    Transform Near = null;

    Rigidbody2D rgdb;
    public float speed = 1.5f;
    public float attackRange = 1f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GameObject.FindGameObjectsWithTag("Chevalier").Length != 0)
        {
            chevaliers = GameObject.FindGameObjectsWithTag("Chevalier");
        }
        else
        {
            chevaliers = new GameObject[] {};
        }
        if (GameObject.FindGameObjectsWithTag("Mage").Length != 0)
        {
            mages = GameObject.FindGameObjectsWithTag("Mage");
        }
        else
        {
            mages = new GameObject[] { };
        }


        /* else if (player == null || Vector2.Distance(player.position, rgdb.position) > Vector2.Distance(player2.position, rgdb.position))
         {
             Near = player2;
         } */
        rgdb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach(GameObject obj in chevaliers)
        {
            if(Vector2.Distance(obj.transform.position, rgdb.position) <= 3)
            {
                Near = obj.transform;
            }
        }

        foreach (GameObject obj in mages)
        {
            if (Vector2.Distance(obj.transform.position, rgdb.position) <= 3)
            {
                Near = obj.transform;
            }
        }
        if(Near != null)
        {
            if (Vector2.Distance(Near.position, rgdb.position) <= attackRange)
            {
                animator.SetTrigger("attack");
            }

        }

    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");

    }
}
