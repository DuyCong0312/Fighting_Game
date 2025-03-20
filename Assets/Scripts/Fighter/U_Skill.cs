using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class U_Skill : MonoBehaviour
{
    protected Animator anim;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        Uskill();
    }

    protected virtual void Uskill()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            anim.SetTrigger("Uskill");
        }
    }
}
