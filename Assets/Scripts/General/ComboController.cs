using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    private CheckGround groundCheck;
    private PlayerState playerState;

    [SerializeField] private int attackNumber;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<CheckGround>();
        playerState = GetComponent<PlayerState>();
    }
    void Update()
    {
        Attack();
        Defend();
    }

    private void StartCombo()
    {
        playerState.isAttacking = false;
        if(attackNumber < 3)
        {
            attackNumber++;
        }
    }

    private void StopCombo()
    {
        playerState.isAttacking = false;
        attackNumber = 0;
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !playerState.isAttacking)
        {
            playerState.isAttacking = true;
            if (groundCheck.isGround)
            {
                anim.SetTrigger(attackNumber + "Attack");
            }
            else
            {
                anim.Play("K+J");
            }
            AudioManager.Instance.PlaySFX(AudioManager.Instance.attack);
        }
    }

    private void Defend()
    {
        if (Input.GetKey(KeyCode.S) && !playerState.isDefending && groundCheck.isGround)
        {
            playerState.isDefending = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            playerState.isDefending = false;
        }
        anim.SetBool("isDefend", playerState.isDefending);
    }
}
