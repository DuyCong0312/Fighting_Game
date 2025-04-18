using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerState playerState;
    private CheckGround groundCheck;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
        groundCheck = GetComponent<CheckGround>();
    }

    public void Attack()
    {
        if (groundCheck.isGround) 
        { 
            playerState.isAttacking = true;
            anim.SetBool("isAttack", true);
        }
        else
        {
            anim.Play("K+J");
        }
    }

    public void Defend()
    {
        playerState.isDefending = true;
        anim.SetBool("isDefend", true);
    }

    public void StopAttack()
    {
        playerState.isAttacking = false;
        anim.SetBool("isAttack", false);
    }

    public void StopDefend()
    {
        playerState.isDefending = false;
        anim.SetBool("isDefend", false);
    }
}
