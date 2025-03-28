using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class PlayerState : MonoBehaviour
{
    public bool isAttacking = false;
    public bool isDefending = false;
    public bool isUsingSkill = false;
    public bool isDead = false;
    private void StopSkill()
    {
        isUsingSkill = false;
    }
}
