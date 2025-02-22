using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComAttack : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 4f;
    [SerializeField] private bool isFacingRight = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        Attack();
    }
    private void Attack()
    {
        if (Vector2.Distance(player.position, transform.position) <= 1f)
        {
            anim.SetBool("isAttack", true);
        }
        else
        {
            anim.SetBool("isAttack", false);
        }
    }
}
