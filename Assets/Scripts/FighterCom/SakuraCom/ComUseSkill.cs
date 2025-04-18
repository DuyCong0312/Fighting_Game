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

    public void PlayUskill()
    {
        playerRage.GetRage(5f);
        anim.SetTrigger("Uskill");
    }

    public void PlayISkill()
    {
        if (playerRage.currentRage >= 30f)
        {
            playerRage.CostRage(30f);
            anim.SetTrigger("Iskill");
        }
        else
        {
            return;
        }

    }

    public void PlayUKskill()
    {
        playerRage.GetRage(5f);
        anim.Play("K+U");
    }

    public void PlayIKSkill()
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

