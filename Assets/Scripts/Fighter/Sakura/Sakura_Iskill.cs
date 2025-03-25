using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_Iskill : I_Skill
{
    [SerializeField] private CheckHit check;
    [SerializeField] private Transform newPos;
    [SerializeField] private Transform attackPos;
    [SerializeField] private float attackRange;
    [SerializeField] private float moveSpeed;

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
                check.RoundAttack(attackPos, attackRange, 5f);
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
}
