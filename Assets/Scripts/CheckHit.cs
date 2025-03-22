using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHit : MonoBehaviour
{
    public Transform meleeAttack01Pos;
    public Transform meleeAttack02Pos;
    public Transform meleeAttack03Pos;
    public Vector2 attackBoxSize = new Vector2(2f, 1f);
    public float attackRange = 1f;
    public LayerMask whatIsEnemies;

    public void StraightAttack(Transform AttackPos, Vector2 AttackSize, float angle, float attackDamage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapBoxAll(AttackPos.position, AttackSize, angle, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit");
            enemy.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    public void RoundAttack(Transform AttackPos, float AttackRange, float attackDamage)
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(AttackPos.position, AttackRange, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit");
            enemy.GetComponent<PlayerHealth>().TakeDamage(attackDamage);
        }
    }

    private void FirstAttack()
    {
        StraightAttack(meleeAttack01Pos, attackBoxSize, 0f,5f);
    }

    private void SecondAttack()
    {
        RoundAttack(meleeAttack02Pos, attackRange, 5f);
    }

    private void ThirdAttack()
    {
        StraightAttack(meleeAttack03Pos, attackBoxSize, 0f, 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(meleeAttack01Pos.position, attackBoxSize);
        Gizmos.DrawWireSphere(meleeAttack02Pos.position, attackRange);
        Gizmos.DrawWireCube(meleeAttack03Pos.position, attackBoxSize);
    }
}
