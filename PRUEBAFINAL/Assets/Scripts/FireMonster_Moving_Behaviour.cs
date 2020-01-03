using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireMonster_Moving_Behaviour : StateMachineBehaviour
{
    [Header("PARAMETROS"),Range(0,25)]
    public float speed;
    [Range(0,15)]
    public float stoppingDistance;
    [Range(0, 15)]
    public float hitDistance;
    [Range(0, 15)]
    public float bulletDistance;
    [Range(0, 15)]
    public float fireBlowDistance;
    [Header("ACTIVE")]
    public bool activeMovement;
    public bool activeHit;
    public bool activeBullet;
    public bool activeFireBlow;
    [Header("WAIT TIME")]
    [Range(0,60)]
    public float waitTimeHit;
    [Range(0, 60)]
    public float waitTimeBulletMode;
    [Range(0, 60)]
    public float waitTimeFireBlow;
    Transform playerPos;

    Boss boss;

    private void Awake()
    {
        playerPos = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = animator.GetComponent<Boss>();
     
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        float distance = Vector2.Distance(animator.transform.position, playerPos.position);
        if ( distance > stoppingDistance && activeMovement)
        {
            Vector2 movement = Vector2.MoveTowards(animator.transform.position, playerPos.position, speed * Time.deltaTime);
            animator.transform.position = movement;
            updatingView(animator); 
        }
        if (distance <= hitDistance && activeHit && !boss.countingWaitTimeHit)
        {
            animator.SetTrigger("Hit");
            if (waitTimeHit != 0)
            {
                boss.countingWaitTimeHit = true;
                boss.doHitCo(waitTimeHit);
            }
            else
            {
                boss.countingWaitTimeHit = false;
            }
            
        }
        if (distance >= bulletDistance && activeBullet && !boss.countingWaitTimeBulletMode)
        {

            //animator.SetBool("Bullet",true);
            animator.SetTrigger("BULLETMODE");
            if (waitTimeBulletMode != 0)
            {
                boss.countingWaitTimeBulletMode = true;
                boss.doBulletModeCo(waitTimeBulletMode);
             
            }
            else
            {
                boss.countingWaitTimeBulletMode = false;
            }
        }
        if (distance >= fireBlowDistance && activeFireBlow && !boss.countingWaitTimeFireBlow)
        {
            animator.SetTrigger("FireBlow");
            if (waitTimeFireBlow != 0)
            {
                boss.countingWaitTimeFireBlow = true;
                boss.doFireBlowCo(waitTimeFireBlow);
            }
            else
            {
                boss.countingWaitTimeFireBlow = false;
            }
        }
    }

    void updatingView(Animator animator)
    {
        Vector2 direction = playerPos.position - animator.transform.position;
        float sign = (direction.y >= 0) ? 1 : -1;

        float angle = Vector2.Angle(Vector2.right, direction) * sign;

        if (angle > -135 && angle < -45)
        {
            ViewDown(animator);
        }
        else if (angle > -45 && angle < 45)
        {
            ViewRight(animator);
        }
        else if (angle > 45 && angle < 135)
        {
            ViewUp(animator);
        }
        else
        {
            ViewLeft(animator);
        }
    }

    void ViewDown(Animator animator)
    {
        animator.SetFloat("x", 0);
        animator.SetFloat("y", -1);
    }
    void ViewUp(Animator animator)
    {
        animator.SetFloat("x", 0);
        animator.SetFloat("y", 1);
    }
    void ViewRight(Animator animator)
    {
        animator.SetFloat("x", 1);
        animator.SetFloat("y", 0);
    }
    void ViewLeft(Animator animator)
    {
        animator.SetFloat("x", -1);
        animator.SetFloat("y", 0);
    }

   

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

}
