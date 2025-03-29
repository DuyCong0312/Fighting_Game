using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHit : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsEnemies;
    public bool hit = false;

    public void StraightAttack(Transform AttackPos, Vector2 AttackSize, float angle, float attackDamage)
    {
        hit = false;
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPos.position, AttackSize, angle, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit");
            hit = true;
            enemy.GetComponentInParent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    public void RoundAttack(Transform AttackPos, float AttackRange, float attackDamage)
    {
        hit = false;
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit");
            hit = true;
            enemy.GetComponentInParent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }
}
