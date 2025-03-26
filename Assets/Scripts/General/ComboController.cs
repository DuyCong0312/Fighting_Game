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

    [SerializeField] private int attackNumber;
    [SerializeField] private bool isAttack = false;
    [SerializeField] private bool isDefend = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<CheckGround>();
    }
    void Update()
    {
        Attack();
        Defend();
    }

    private void StartCombo()
    {
        isAttack = false;
        if(attackNumber < 3)
        {
            attackNumber++;
        }
    }

    private void StopCombo()
    {
        isAttack = false;
        attackNumber = 0;
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.J) && !isAttack)
        {
            isAttack = true;
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
        if (Input.GetKey(KeyCode.S) && !isDefend && groundCheck.isGround)
        {
            isDefend = true;
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            isDefend= false;
        }
        anim.SetBool("isDefend", isDefend);
    }
}
