using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_Uskill : U_Skill
{
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private Transform skillPos;

    private void ActiveSkill()
    {
        Instantiate(skillPrefab, skillPos.position, transform.rotation);
    }
}
