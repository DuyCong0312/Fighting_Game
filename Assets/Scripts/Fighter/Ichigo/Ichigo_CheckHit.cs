using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ichigo_CheckHit : MonoBehaviour
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
        checkHit.RoundAttack(meleeAttack01Pos, attackRange, 5f, transform.right);
    }

    private void SecondAttack()
    {
        checkHit.RoundAttack(meleeAttack02Pos, attackRange, 5f, transform.right);
    }

    private void ThirdAttack()
    {
        checkHit.StraightAttack(meleeAttack03Pos, attackBoxSize, 0f, 5f, new Vector2(1,1));
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(meleeAttack01Pos.position, attackRange);
        Gizmos.DrawWireSphere(meleeAttack02Pos.position, attackRange);
        Gizmos.DrawWireCube(meleeAttack03Pos.position, attackBoxSize);
    }
}
