using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalDeath : StateMachineBehaviour
{
    GameObject Heal;
    GameObject DSprite;
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
        if(animator.gameObject.layer!=3){
            Heal=animator.GetComponent<HP>().Heal;
            Instantiate(Heal, animator.gameObject.transform.position, animator.gameObject.transform.rotation);
            DSprite=animator.GetComponent<HP>().DSprite;
            Instantiate(DSprite, animator.gameObject.transform.position, animator.gameObject.transform.rotation);
            Destroy(animator.gameObject);
        }else{
            animator.GetComponent<Player>().deathMenu();
            animator.gameObject.SetActive(false);
        }
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
