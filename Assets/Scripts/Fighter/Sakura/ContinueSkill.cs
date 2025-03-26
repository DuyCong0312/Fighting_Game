using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueSkill : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private Transform playerTransform;
    private CheckGround groundCheck;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        playerTransform = animator.transform;
        groundCheck = animator.GetComponent<CheckGround>();
        rb = playerTransform.GetComponent<Rigidbody2D>();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (groundCheck.isGround)
        {
            animator.Play("K+U2");
            rb.velocity = Vector2.zero; 
            Vector3 currentRotation = playerTransform.rotation.eulerAngles;
            playerTransform.rotation = Quaternion.Euler(currentRotation.x, currentRotation.y, 0);
        }  
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
