using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class FireMonster_FireBlow_Behaviour : StateMachineBehaviour
{

    /*public GameObject bullet;
    BulletMovement bulletMovement;
    int n;

    Rigidbody2D monsterRigid;*/
   
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        /*monsterRigid = GameObject.Find("FireMonster").GetComponent<Rigidbody2D>();

        n = animator.GetInteger("Nbullet");
        bullet.name = "sunBurn_" + n ;
        animator.SetInteger("Nbullet", n + 1);
        Vector3 origin = new Vector3(monsterRigid.position.x * 1.3f, monsterRigid.position.y * 1.3f, 0);
        Instantiate(bullet, origin, Quaternion.identity);

        bulletMovement = GameObject.Find(bullet.name).GetComponent<BulletMovement>();
        bulletMovement.setGO(true);*/
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


    }


}

