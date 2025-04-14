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
    [SerializeField] private bool canAttack = true;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<CheckGround>();
        playerState = GetComponent<PlayerState>();
    }
    void Update()
    {
        if (GameManager.Instance.gameEnded
            || playerState.isUsingSkill
            || playerState.isGettingHurt)
        {
            return;
        }
        Attack();
        Defend();
    }

    private void StartCombo()
    {
        canAttack = true;
        if(attackNumber < 3)
        {
            attackNumber++;
        }
    }

    private void StopCombo()
    {
        playerState.isAttacking = false;
        canAttack = true;
        attackNumber = 0;
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && canAttack)
        {
            canAttack = false;
            playerState.isAttacking = true;
            if (groundCheck.isGround)
            {
                anim.SetTrigger(attackNumber + "Attack");
                MoveWhenAttack();
            }
            else
            {
                anim.Play("K+J");
            }
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

    private void MoveWhenAttack()
    {
        float direction = playerState.isFacingRight? 1 : -1;
        transform.position += new Vector3(direction *  attackNumber * 0.1f, 0f, 0f);
    }

}
