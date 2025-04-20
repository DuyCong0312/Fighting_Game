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
    [SerializeField] private float attackMoveDuration = 0.1f;
    private bool hasInterrupted = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<CheckGround>();
        playerState = GetComponent<PlayerState>();
    }
    void Update()
    {
        GetHurtWhenAttacking();

        if (!GameManager.Instance.gameStart
            || GameManager.Instance.gameEnded
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
        anim.ResetTrigger("0Attack");
        anim.ResetTrigger("1Attack");
        anim.ResetTrigger("2Attack");
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
                StartCoroutine(MoveWhenAttack());
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

    private IEnumerator MoveWhenAttack()
    {
        float direction = playerState.isFacingRight ? 1 : -1;
        float timer = 0f;

        while (timer < attackMoveDuration)
        {
            rb.velocity = new Vector2(direction * attackNumber * 2f, rb.velocity.y);
            timer += Time.deltaTime;
            yield return null;
        }
    }

    private void GetHurtWhenAttacking()
    {
        if (playerState.isGettingHurt && !hasInterrupted)
        {
            hasInterrupted = true;
            StopCombo();
        }

        if (!playerState.isGettingHurt && hasInterrupted)
        {
            hasInterrupted = false;
        }
    }

}
