using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class ComMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private Transform player;
    private CheckGround groundCheck;
    private PlayerState playerState;
    private SpawnEffectAfterImage effectAfterImage;

    [Header("Jump Setting")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 11f;

    [Header("Effect")]
    [SerializeField] private Transform jumpPos;
    [SerializeField] private Transform dashPos;

    [Header("Dash Setting")]
    [SerializeField] private float dashPower = 10f;
    [SerializeField] private float dashTime = 0.1f;
    [SerializeField] private float dashCooldown = 1f;
    [SerializeField] private bool canDash = true;
    public bool isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = GetComponent<CheckGround>();
        playerState = GetComponent<PlayerState>();
        effectAfterImage = GetComponent<SpawnEffectAfterImage>();
        playerState.isFacingRight = false;
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

    void Update()
    {
        UpdateAnimation();
        if (GameManager.Instance.gameEnded
            || playerState.isAttacking
            || playerState.isUsingSkill
            || playerState.isDefending
            || playerState.isGettingHurt)
        {
            return;
        }
        Flipped();
    }

    public void MoveToPlayer()
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
            StopMoveToPlayer();
        }
    }

    public void StopMoveToPlayer()
    {
        anim.SetBool("Running", false);
    }
    private void Flipped()
    {
        if (player.position.x > this.transform.position.x && playerState.isFacingRight == false)
        {
            this.transform.eulerAngles = new Vector3(0, 0, 0);
            playerState.isFacingRight = true;
        }
        else if (player.position.x < this.transform.position.x && playerState.isFacingRight == true)
        {
            this.transform.eulerAngles = new Vector3(0, 180, 0);
            playerState.isFacingRight = false;
        }
    }

    public void HandleDash()
    {
        if (canDash)
        {
            StartCoroutine(Dash());
            AudioManager.Instance.PlaySFX(AudioManager.Instance.dash);
            if (groundCheck.isGround)
            {
                EffectManager.Instance.SpawnEffect(EffectManager.Instance.groundDash, dashPos, Quaternion.Euler(0, 180, 0) * transform.rotation);
            }
            else
            {
                EffectManager.Instance.SpawnEffect(EffectManager.Instance.airDash, dashPos, Quaternion.Euler(0, 180, 0) * transform.rotation);
            }
        }
    }
    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        float direction = playerState.isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * dashPower, 0f);
        anim.SetBool("isDashing", true);
        effectAfterImage.StartAfterImageEffect();
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        anim.SetBool("isDashing", false);
        effectAfterImage.StopAfterImageEffect();
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    public void HandleJump()
    {
        if(groundCheck.isGround == true)
        {
            Jump();
            groundCheck.isJumping = true;
        }
    }
    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        anim.SetBool("Jumping", true);
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.jump, jumpPos, transform.rotation);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
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
        if (groundCheck.isGround && anim.GetBool("Falling"))
        {
            anim.SetBool("Falling", false);
            EffectManager.Instance.SpawnEffect(EffectManager.Instance.touchGround, jumpPos, transform.rotation);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.touchGround);
        }
    }

    private void Footstep()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.step1);
    }
    private void Footstep3()
    {
        AudioManager.Instance.PlaySFX(AudioManager.Instance.step3);
    }
}
