using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;

    [SerializeField] private int attackNumber;
    [SerializeField] private bool isAttack = false;
    [SerializeField] private bool isDefend = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
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
            anim.SetTrigger(attackNumber + "Attack");
            isAttack = true;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.attack);
        }
    }

    private void Defend()
    {
        if (Input.GetKey(KeyCode.S) && !isDefend)
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
