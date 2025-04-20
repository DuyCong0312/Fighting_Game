using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private PlayerState playerState;
    private CheckGround groundCheck;
    private Transform player;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
        groundCheck = GetComponent<CheckGround>(); 
        StartCoroutine(WaitForPlayers());
    }

    private IEnumerator WaitForPlayers()
    {
        while (GameObject.FindGameObjectsWithTag("Player").Length < 1)
        {
            yield return null;
        }

        FindPlayers();
    }

    private void FindPlayers()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        if (Mathf.Abs(player.position.x - transform.position.x) > 1f)
        {
            StopAttack();
        }
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
        StartCoroutine(DefendCoroutine());
    }

    public void StopAttack()
    {
        playerState.isAttacking = false;
        anim.SetBool("isAttack", false);
    }

    private IEnumerator DefendCoroutine()
    {
        playerState.isDefending = true;
        anim.SetBool("isDefend", true);
        yield return new WaitForSeconds(1f);
        playerState.isDefending = false;
        anim.SetBool("isDefend", false);
    }
}
