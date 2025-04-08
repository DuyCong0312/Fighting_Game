using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_Iskill : I_Skill
{
    [Header("I Skill")]
    [SerializeField] private CheckHit check;
    [SerializeField] private Transform newPos;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;


    [Header("I+K Skill")]
    [SerializeField] private float force;
    [SerializeField] private GameObject effectIK1;
    [SerializeField] private GameObject effectIK2;
    [SerializeField] private Transform effectIKPos;
    [SerializeField] private Transform spwanEffectPos;

    private Vector3 newPosition;
    private bool isMoving = false;
    private bool canMove = true;

    public void ActiveMove()
    {
        if (!isMoving)
        {
            isMoving = true;
            StartCoroutine(Move());
        }
    }

    private IEnumerator Move()
    {
        newPosition = newPos.position;
        while (Vector2.Distance(transform.position, newPosition) > 0.01f && canMove)
        {
            transform.position = Vector2.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
            if (Time.frameCount % 2 == 0)
            {
                check.RoundAttack(attackPos, attackRange, 5f, Vector2.zero);
                if (check.hit)
                {
                    canMove = false;
                    break;
                }
            }

            yield return null;
        }
        isMoving = false;
        canMove = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("MainCamera"))
        {
            isMoving = false;
            canMove = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void ActiveSakuraIKSkill()
    {
        this.transform.rotation = this.transform.rotation; 
        rb.velocity = new Vector2 (rb.velocity.x, - force);
    }

    private void ActiveEffectIK1() 
    {
        SpawnSkillEffect(effectIK1,effectIKPos);
    }

    private void ActiveEffectIK2()
    {
        SpawnSkillEffect(effectIK2, spwanEffectPos);
    }

    private void SpawnSkillEffect(GameObject name, Transform nameTransform)
    {
        Instantiate(name, nameTransform.position, nameTransform.rotation, nameTransform);
    }
}
