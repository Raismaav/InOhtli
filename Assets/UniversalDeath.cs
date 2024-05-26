using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalDeath : StateMachineBehaviour
{
    GameObject Heal;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(animator.gameObject.layer==12){
            Heal=animator.GetComponent<JaguarEnemy>().Heal;
        }if(animator.gameObject.layer==11){
            Heal=animator.GetComponent<basicEnemy>().Heal;
        }if(animator.gameObject.layer==13){
            Heal=animator.GetComponent<flyEnemy>().Heal;
        }
        Instantiate(Heal, animator.gameObject.transform.position, animator.gameObject.transform.rotation);
        Destroy(animator.gameObject);
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
