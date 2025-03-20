using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_Uskill : U_Skill
{
    [SerializeField] private GameObject SkillPrefab;
    [SerializeField] private Transform skillPos;

    protected override void Uskill()
    {
        base.Uskill();
    }

    private void ActiveSkill()
    {
        Instantiate(SkillPrefab, skillPos.position, transform.rotation);
    }
}
