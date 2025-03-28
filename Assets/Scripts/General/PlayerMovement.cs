using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private enum State { Idle, Running, Jumping, Falling }
    private State currentState;

    private Rigidbody2D rb;
    private Animator anim;
    private CheckGround groundCheck;
    private PlayerState playerState;

    [SerializeField] private float speed = 4f;
    [SerializeField] private float jumpForce = 6f;
    [SerializeField] private bool isDoubleJump;
    public bool isFacingRight = true;

    [Header("Effect")]
    [SerializeField] private Transform jumpPos;
    [SerializeField] private Transform dashPos;

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
        groundCheck = GetComponent<CheckGround>();
        playerState = GetComponent<PlayerState>();
    }

    void Update()
    {
        UpdateAnimation();
        if ( playerState.isAttacking || 
            playerState.isUsingSkill || 
            playerState.isDefending)
        {
            return;
        }
        Movement();
        HandleJump();
        HandleDash();
        UpdateState();
    }

    private void Movement()
    {
        if (isDashing)
        {
            return;
        }

        float movement = 0f;

        if (Input.GetKey(KeyCode.A))
        {
            if (isFacingRight) 
            {
                this.transform.eulerAngles = new Vector3(0, 180, 0);
                isFacingRight = false;
            }
            movement = -1f; 
        }
        else if (Input.GetKey(KeyCode.D))
        {
            if (!isFacingRight)
            {
                this.transform.eulerAngles = new Vector3(0, 0, 0);
                isFacingRight = true;
            }
            movement = 1f;
        }
        rb.velocity = new Vector2(speed * movement, rb.velocity.y);
    }

    private void HandleJump()
    {
        if (isDashing)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.K) && groundCheck.isGround == true)
        {
            Jump();
            groundCheck.isGround = false;
            isDoubleJump = true;
        }
        else if (Input.GetKeyDown(KeyCode.K) && isDoubleJump && currentState == State.Falling)
        {
            Jump();
            isDoubleJump = false;
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        currentState = State.Jumping;
        EffectManager.Instance.SpawnEffect(EffectManager.Instance.jump, jumpPos, transform.rotation);
        AudioManager.Instance.PlaySFX(AudioManager.Instance.jump);
    }

    private void HandleDash()
    {
        if(Input.GetKeyDown(KeyCode.N) && canDash)
        {
            StartCoroutine(Dash());
            anim.SetBool("isDashing", true);
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
        float direction = isFacingRight ? 1 : -1;
        rb.velocity = new Vector2(direction * dashPower, 0f);
        yield return new WaitForSeconds(dashTime);
        rb.gravityScale = originalGravity;
        isDashing = false;
        anim.SetBool("isDashing", false);
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }

    private void UpdateAnimation()
    {
        anim.SetInteger("CurrentState", (int)currentState);
    }

    private void UpdateState()
    {
        if (currentState == State.Jumping)
        {
            if (rb.velocity.y < 0.1f)
            {
                currentState = State.Falling;
            }
        }
        else if (currentState == State.Falling)
        {
            if (groundCheck.isGround)
            {
                currentState = State.Idle;
                EffectManager.Instance.SpawnEffect(EffectManager.Instance.touchGround, jumpPos, transform.rotation);
                AudioManager.Instance.PlaySFX(AudioManager.Instance.touchGround);
            }
        }
        else if (Mathf.Abs(rb.velocity.x) > 1f)
        {
            currentState = State.Running;
        }
        else
        {
            currentState = State.Idle;
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
