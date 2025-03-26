using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Skill : MonoBehaviour
{
    protected Animator anim;
    protected CheckGround groundCheck;
    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        groundCheck = GetComponent<CheckGround>();
    }

    protected virtual void Update()
    {
        Uskill();
    }

    protected virtual void Uskill()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (groundCheck.isGround)
            {
                anim.SetTrigger("Uskill");
            }
            else
            {
                anim.Play("K+U");
            }
        }
    }
}
