using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SemiBossJumpBehavour : StateMachineBehaviour
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    private Rigidbody2D rb;
    private Boss Boss;
    private float gravity=-9.8f;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       Boss=animator.GetComponent<Boss>();
       rb=Boss.rb;
       Boss.LockPlayer();
       Vector3 direction= Boss.Player.position-animator.transform.position;
       rb.velocity=speedCalculate(direction);
    }
    private UnityEngine.Vector3 speedCalculate(UnityEngine.Vector3 Speed){
        float speedx,speedy;
        speedy=Mathf.Sqrt(-2*gravity*6);
        speedx=Speed.x/((-speedy/gravity)+Mathf.Sqrt(2*(Speed.y-6)/gravity));
        return new UnityEngine.Vector3(speedx,speedy,0);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       animator.SetFloat("VerticalSpeed",rb.velocityY);
    }

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
