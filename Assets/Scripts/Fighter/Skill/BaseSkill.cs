using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseSkill : MonoBehaviour
{
    protected Rigidbody2D rb;
    protected Animator anim;
    protected CheckGround groundCheck;
    protected PlayerState playerState;

    protected abstract KeyCode SkillKey { get; }
    protected abstract string GroundAnimationTrigger { get; }
    protected abstract string AirAnimationName { get; }

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        groundCheck = GetComponent<CheckGround>();
        playerState = GetComponent<PlayerState>();
    }

    protected virtual void Update()
    {
        if (playerState.isAttacking)
        {
            return;
        }
        PerformSkill();
    }

    protected virtual void PerformSkill()
    {
        if (Input.GetKeyDown(SkillKey))
        {
            playerState.isUsingSkill = true;
            if (groundCheck.isGround)
            {
                anim.SetTrigger(GroundAnimationTrigger);
            }
            else
            {
                bool hasAnimation = false;
                foreach (AnimationClip clip in anim.runtimeAnimatorController.animationClips)
                {
                    if (clip.name == AirAnimationName)
                    {
                        hasAnimation = true;
                        break;
                    }
                }
                if (!hasAnimation)
                {
                    playerState.isUsingSkill = false;
                    return;
                }
                anim.Play(AirAnimationName);
            }
        }
    }
}
