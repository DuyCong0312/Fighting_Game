using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_Iskill : I_Skill
{
    [SerializeField] private GameObject skillPrefab;
    [SerializeField] private GameObject skillPrefab2;
    [SerializeField] private Transform skillPos;

    private void ActivateSkill(GameObject skill, Quaternion rotation)
    {
        Instantiate(skill, skillPos.position, rotation);
    }

    private void ActiveSkill01()
    {
        ActivateSkill(skillPrefab, transform.rotation);
    }

    private void ActiveSkill02()
    {
        ActivateSkill(skillPrefab, Quaternion.Euler(180, 0, 0) * transform.rotation);
    }

    private void ActiveSkill03()
    {
        ActivateSkill(skillPrefab2, transform.rotation);
    }

}
