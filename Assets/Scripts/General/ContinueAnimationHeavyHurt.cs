using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueAnimationHeavyHurt : StateMachineBehaviour
{
    private Rigidbody2D rb;
    private Transform playerTransform;
    private Transform touchGroundPos;
    private CheckGround groundCheck;
    private PlayerMovement playerMovement;
    [SerializeField] private string nameAnimatorClip;
    [SerializeField] private GameObject effectTouchGround;
    [SerializeField] private float blowUpPower;

    private bool blowUpCalled = false;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (!blowUpCalled)
        {
            playerTransform = animator.transform;
            groundCheck = animator.GetComponent<CheckGround>();
            rb = playerTransform.GetComponent<Rigidbody2D>();
            playerMovement = playerTransform.GetComponent<PlayerMovement>();
            BlowUp();
            blowUpCalled = true;
        }
        if (touchGroundPos == null)
        {
           touchGroundPos = playerTransform.Find("JumpPos");
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (groundCheck.isGround)
        {
            animator.Play(nameAnimatorClip);
            Instantiate(effectTouchGround, touchGroundPos.position, touchGroundPos.rotation);
            rb.velocity = Vector2.zero;
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

    private void BlowUp()
    {
        float direction = playerMovement.isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * blowUpPower, blowUpPower / 2f);
    }

}
