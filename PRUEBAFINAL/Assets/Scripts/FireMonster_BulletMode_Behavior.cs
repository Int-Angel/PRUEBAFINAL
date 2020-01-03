using System.Collections;
using System.Collections.Generic;
using UnityEngine;


class FireMonster_BulletMode_Behavior : StateMachineBehaviour
{
    [Header("PARAMETROS"), Range(0, 25)]
    public float speed;
  
    Transform playerPos;
    Vector2 direction;
 

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
 
        direction = playerPos.position - animator.transform.position;
        direction.x = direction.x * 3;
        direction.y = (direction.y * 3) - 3;
         
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, direction, speed * Time.deltaTime);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {


    }


}

