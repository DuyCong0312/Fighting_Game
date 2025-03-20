using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_Uskill : U_Skill
{
    [SerializeField] private GameObject SkillPrefab;
    [SerializeField] private Transform skillPos;
    [SerializeField] private Transform newPos;
    [SerializeField] private float backSpeed;

    private Vector3 newPosition;

    protected override void Uskill()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            newPosition = newPos.position;
            anim.SetTrigger("Uskill");
            StartCoroutine(StepBack());
        }
    }

    private IEnumerator StepBack()
    {
        while (Vector2.Distance(transform.position, newPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPosition, backSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = newPosition;
    }

    private void ActiveSkill(int extraInstances, float offset)
    {
        Instantiate(SkillPrefab, skillPos.position, transform.rotation);

        for (int i = 1; i <= extraInstances; i++)
        {
            Instantiate(SkillPrefab, new Vector2(skillPos.position.x, skillPos.position.y + offset), transform.rotation);
            Instantiate(SkillPrefab, new Vector2(skillPos.position.x, skillPos.position.y - offset), transform.rotation);
        }
    }

    private void ActiveSkillP1()
    {
        ActiveSkill(0, 0f);
    }

    private void ActiveSkillP2()
    {
        ActiveSkill(1, 0.5f);
    }
}
