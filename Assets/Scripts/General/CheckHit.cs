using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHit : MonoBehaviour
{
    [SerializeField] protected LayerMask whatIsEnemies;
    protected SpriteRenderer spriteRenderer;
    public bool hit = false;

    protected virtual void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();   
    }

    protected void StraightAttack(Transform AttackPos, Vector2 AttackSize, float angle, float attackDamage, Vector2 direction)
    {
        hit = false;
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPos.position, AttackSize, angle, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit");
            hit = true;
            this.spriteRenderer.sortingOrder = 1;
            enemy.GetComponentInParent<PlayerHealth>().TakeDamage(attackDamage, direction);
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }

    protected void RoundAttack(Transform AttackPos, float AttackRange, float attackDamage, Vector2 direction)
    {
        hit = false;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit");
            hit = true;
            this.spriteRenderer.sortingOrder = 1;
            enemy.GetComponentInParent<PlayerHealth>().TakeDamage(attackDamage, direction);
            enemy.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
    }
}
