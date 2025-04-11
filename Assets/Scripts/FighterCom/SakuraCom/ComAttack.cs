using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;
    private PlayerState playerState;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (GameManager.Instance.gameEnded
            || playerState.isUsingSkill
            || playerState.isDefending
            || playerState.isGettingHurt)
        {
            return;
        }
        Attack();
    }

    private void Attack()
    {
        if (Vector2.Distance(player.position, transform.position) <= 1f)
        {
            playerState.isAttacking = true;
        }
        else
        {
            playerState.isAttacking= false;
        }
        anim.SetBool("isAttack", playerState.isAttacking);
    }
}
