using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossfaucheuseAnim : StateMachineBehaviour
{

    GameObject[] mages;
    GameObject[] chevaliers;
    Transform Near = null;

    Rigidbody2D rgdb;
    public float speed;
    public float attackRange;
    float distanceDetection = 8f;
    public float distanceNear;
    public bool isFlipped = false;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (GameObject.FindGameObjectsWithTag("Chevalier").Length != 0)
        {
            chevaliers = GameObject.FindGameObjectsWithTag("Chevalier");
        }
        else
        {
            chevaliers = new GameObject[] { };
        }
        if (GameObject.FindGameObjectsWithTag("Mage").Length != 0)
        {
            mages = GameObject.FindGameObjectsWithTag("Mage");
        }
        else
        {
            mages = new GameObject[] { };
        }
        rgdb = animator.GetComponent<Rigidbody2D>();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        foreach (GameObject obj in chevaliers)
        {
            if (Vector2.Distance(obj.transform.position, rgdb.position) <= distanceDetection)
            {
                Near = obj.transform;
                distanceNear = Vector2.Distance(obj.transform.position, rgdb.position);
                animator.SetFloat("distance", distanceNear);
            }
        }

        foreach (GameObject obj in mages)
        {
            if (Vector2.Distance(obj.transform.position, rgdb.position) <= distanceDetection)
            {
                Near = obj.transform;
                distanceNear = Vector2.Distance(obj.transform.position, rgdb.position);
                animator.SetFloat("distance", distanceNear);

            }
        }
        if (Near != null)
        {
            Vector2 target = new Vector2(Near.position.x, rgdb.position.y);
            Vector2 newPos = Vector2.MoveTowards(rgdb.position, target, speed * Time.fixedDeltaTime);
            rgdb.MovePosition(newPos);
            if (rgdb.position.x > Near.position.x && isFlipped)
            {
                rgdb.transform.localScale = new Vector3(0.1f, 0.1f, 1f);
                isFlipped = false;
            }
            else if (rgdb.position.x < Near.position.x && !isFlipped)
            {
                rgdb.transform.localScale = new Vector3(-0.1f, 0.1f, 1f);
                isFlipped = true;
            }
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
