using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_Uskill : U_Skill
{
    [Header("U Skill")]
    [SerializeField] private GameObject SkillPrefab;
    [SerializeField] private Transform skillPos;
    [SerializeField] private Transform newPos;
    [SerializeField] private float backSpeed;

    private Vector3 newPosition;
    private bool isMoving = false;
    private bool canMove = true;
    private Rigidbody2D rb;

    [Header("U+K Skill")]
    [SerializeField] private float force;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Uskill()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            if (groundCheck.isGround)
            {
                newPosition = newPos.position;
                anim.SetTrigger("Uskill");
                StartCoroutine(StepBack());
            }
            else
            {
                anim.Play("K+U");
            }
        }
    }

    private IEnumerator StepBack()
    {
        while (Vector2.Distance(transform.position, newPosition) > 0.01f && canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPosition, backSpeed * Time.deltaTime);
            yield return null;
        }
        isMoving = false;
        canMove = true;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MainCamera"))
        {
            isMoving = false;
            canMove = false;
        }
    }

    private void ActiveUKSkill()
    {
        this.transform.rotation =  this.transform.rotation * Quaternion.Euler(0, 0, 45);
        rb.velocity = new Vector2(rb.velocity.x, -force);
    }

}
