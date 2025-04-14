using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComUseSkill : MonoBehaviour
{
    private Animator anim;
    private PlayerRage playerRage;

    private void Start()
    {
        anim = GetComponent<Animator>();
        playerRage = GetComponentInParent<PlayerRage>();
    }

    private void PlayUskill()
    {
        playerRage.GetRage(5f);
        anim.Play("U");
    }

    private void PlayISkill()
    {
        if (playerRage.currentRage >= 30f)
        {
            playerRage.CostRage(30f);
            anim.Play("I");
        }
        else
        {
            return;
        }

    }

    private void PlayUKskill()
    {
        playerRage.GetRage(5f);
        anim.Play("K+U" +
            "");
    }

    private void PlayIKSkill()
    {
        if (playerRage.currentRage >= 30f)
        {
            playerRage.CostRage(30f);
            anim.Play("K+I");
        }
        else
        {
            return;
        }

    }
}

