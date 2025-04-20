using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComUseSkill : MonoBehaviour
{
    private Animator anim;
    private PlayerRage playerRage;
    private PlayerState playerState;
    private CheckGround groundCheck;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerState = GetComponent<PlayerState>();
        groundCheck = GetComponent<CheckGround>();
        playerRage = GetComponentInParent<PlayerRage>();
    }

    public void PlayUskill()
    {
        playerState.isUsingSkill = true;
        playerRage.GetRage(5f);
        if (groundCheck.isGround)
        {
            anim.SetTrigger("Uskill");
        }
        else
        {
            anim.Play("K+U");
        }
    }

    public void PlayISkill()
    {
        if (playerRage.currentRage >= 30f)
        {
            playerState.isUsingSkill = true;
            playerRage.CostRage(30f);
            if (groundCheck.isGround)
            {
                anim.SetTrigger("Iskill");
            }
            else
            {
                anim.Play("K+I");
            }
        }
        else
        {
            return;
        }

    }
}

