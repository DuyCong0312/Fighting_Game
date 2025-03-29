using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sakura_CheckHit : MonoBehaviour
{
    private CheckHit checkHit;

    [SerializeField] private Transform meleeAttack01Pos;
    [SerializeField] private Transform meleeAttack02Pos;
    [SerializeField] private Transform meleeAttack03Pos;
    [SerializeField] private Vector2 attackBoxSize;
    [SerializeField] private float attackRange;

    private void Start()
    {
        checkHit = GetComponent<CheckHit>();
    }
    private void FirstAttack()
    {
        checkHit.StraightAttack(meleeAttack01Pos, attackBoxSize, 0f, 5f);
    }

    private void SecondAttack()
    {
        checkHit.RoundAttack(meleeAttack02Pos, attackRange, 5f);
    }

    private void ThirdAttack()
    {
        checkHit.StraightAttack(meleeAttack03Pos, attackBoxSize, 0f, 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(meleeAttack01Pos.position, attackBoxSize);
        Gizmos.DrawWireSphere(meleeAttack02Pos.position, attackRange);
        Gizmos.DrawWireCube(meleeAttack03Pos.position, attackBoxSize);
    }
}
