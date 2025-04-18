using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SakuraCom_Uskill : MonoBehaviour
{
    [Header("U Skill")]
    [SerializeField] private GameObject SkillPrefab;
    [SerializeField] private Transform skillPos;
    [SerializeField] private Transform newPos;
    [SerializeField] private float backSpeed;

    private Rigidbody2D rb;
    private SpawnEffectAfterImage effectAfterImage;
    private Vector3 newPosition;
    private bool canMove = true;
    private Vector2 movement;

    [Header("U+K Skill")]
    [SerializeField] private float force;
    [SerializeField] private GameObject effectUK1;
    [SerializeField] private GameObject effectUK2;
    [SerializeField] private Transform spwanEffectPos;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        effectAfterImage = GetComponent<SpawnEffectAfterImage>();
    }

    private void ActiveStepBack()
    {
        StartCoroutine(StepBack());
        effectAfterImage.StartAfterImageEffect();
    }
    private IEnumerator StepBack()
    {
        newPosition = newPos.position;
        while (Vector2.Distance(transform.position, newPosition) > 0.01f && canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPosition, backSpeed * Time.deltaTime);
            yield return null;
        }
        effectAfterImage.StopAfterImageEffect();
        canMove = true;
    }

    private void ActiveSakuraUSkill(int extraInstances, float offset)
    {
        Instantiate(SkillPrefab, skillPos.position, transform.rotation);

        for (int i = 1; i <= extraInstances; i++)
        {
            Instantiate(SkillPrefab, new Vector2(skillPos.position.x, skillPos.position.y + offset), transform.rotation);
            Instantiate(SkillPrefab, new Vector2(skillPos.position.x, skillPos.position.y - offset), transform.rotation);
        }
    }

    private void ActiveSakuraUSkillP1()
    {
        ActiveSakuraUSkill(0, 0f);
    }

    private void ActiveSakuraUSkillP2()
    {
        ActiveSakuraUSkill(1, 0.5f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MainCamera"))
        {
            canMove = false;
        }
    }

    private void ActiveSakuraUKSkill()
    {
        this.transform.rotation = this.transform.rotation * Quaternion.Euler(0, 0, 45);
        Vector2 direction = -this.transform.up;
        float angle = Mathf.Atan2(direction.y, direction.x);
        movement.x = force * Mathf.Cos(angle);
        movement.y = force * Mathf.Sin(angle);

        rb.velocity = movement.normalized * force;
    }

    private void ActiveEffectUK()
    {
        SpawnSkillEffect(effectUK1);
        SpawnSkillEffect(effectUK2);
    }

    private void SpawnSkillEffect(GameObject name)
    {
        Instantiate(name, spwanEffectPos.position, spwanEffectPos.rotation);
    }

}
