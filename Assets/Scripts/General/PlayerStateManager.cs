using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour 
{
    public static PlayerStateManager Instance { get; private set; }

    private enum State { Idle, Running, Jumping, Falling }
    private State currentState;

    private Rigidbody2D rb;
    private CheckGround groundCheck;

    public int CurrentState => (int)currentState;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<CheckGround>();
    }
    private void Update()
    {
        UpdateState();
    }

    public void UpdateState()
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

    public void SetJumping()
    {
        currentState = State.Jumping;
    }
}
