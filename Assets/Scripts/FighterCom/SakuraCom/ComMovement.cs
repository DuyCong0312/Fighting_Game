using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ComMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Transform player;
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 11f;
    [SerializeField] private bool isFacingRight = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float circleRadius;
    public bool isGround;
    public bool isJumping = false;

    [Header("Dash Setting")]
    [SerializeField] private float dashPower = 10f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private bool isDashing;
    [SerializeField] private bool canDash = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        MoveToPlayer();
        Flipped();
        UpdateAnimation();
        HandleJump();
        GroundCheck();
    }

    private void MoveToPlayer()
    {
        if (isDashing)
        {
            return;
        }
        if(Vector2.Distance(player.position, transform.position) > 1f)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
            anim.SetBool("Running", true);
        }
        else
        {
            anim.SetBool("Running", false);
        }
    }
    private void Flipped()
    {
        if (player.position.x > this.transform.position.x && isFacingRight == false)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            isFacingRight = true;
        }
        else if (player.position.x < this.transform.position.x && isFacingRight == true)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            isFacingRight = false;
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        float direction = isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * dashPower, 0f);
        anim.SetBool("isDashing", true);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        anim.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void HandleJump()
    {
        float distanceFromPlayer = Mathf.Abs(player.position.y - this.transform.position.y);
        if(distanceFromPlayer > 0.5f && isGround == true)
        {
            Jump();
            isJumping = true;
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool("Jumping", true);
    }

    private void UpdateAnimation()
    {
        if (anim.GetBool("Jumping"))
        {
            if (rb.velocity.y < 0.1f)
            {
                anim.SetBool("Falling", true);
                anim.SetBool("Jumping", false);
            }
        }
        if (isGround && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
        }
    }

    private void GroundCheck()
    {
        isGround = Physics2D.OverlapCircle(groundCheck.position, circleRadius, groundLayer);

        isJumping = isGround ? false : true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(groundCheck.position, circleRadius);
    }

    private void Footstep()
    {
        audioManager.PlaySFX(audioManager.step1);
    }
    private void Footstep3()
    {
        audioManager.PlaySFX(audioManager.step3);
    }
}
