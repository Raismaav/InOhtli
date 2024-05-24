using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiBossBehaviour : StateMachineBehaviour
{
   private Rigidbody2D rb;
    private Boss FBoss;
    [SerializeField] private float MoveSpeed;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       FBoss=animator.GetComponent<Boss>();
       rb=FBoss.rb;
       FBoss.LockPlayer();
       FBoss.SetBox(true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       rb.velocity=new Vector2(MoveSpeed,rb.velocityY)*animator.transform.right;
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       rb.velocity=new Vector2(0,rb.velocityY);
       FBoss.SetBox(false);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
