using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class I_Skill : MonoBehaviour
{
    private Animator anim;

    void Start()
    {
        anim =GetComponent<Animator>();
    }
    
    void Update()
    {
        Iskill();   
    }
    private void Iskill()
    {
        if (Input.GetKeyDown(KeyCode.I))
        { 
            anim.SetTrigger("Iskill");
        }
    }
}
