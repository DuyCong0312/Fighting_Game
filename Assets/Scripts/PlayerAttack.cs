using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private GameObject SkillPrefab;
    [SerializeField] private Transform skillPos;

    //public Transform meleeAttack01Pos;
    //public Transform meleeAttack02Pos;
    public float attackRange = 1f;
    public LayerMask whatIsEnemies;
    public float attackDamage = 10f;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        Uskill();
        Iskill();
        
    }

    /*public void MeleeAttack01()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttack01Pos.position, attackRange, whatIsEnemies);
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit");
            enemy.GetComponent<Boss_health>().TakeDamage(attackDamage);
        }
    }*/

    /*public void MeleeAttack02()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(meleeAttack02Pos.position, attackRange, whatIsEnemies);
        foreach( Collider2D enemy in hitEnemies)
        {
            Debug.Log("hit02");
        }
    }*/

    private void Uskill()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            anim.SetTrigger("Uskill");
        }
    }

    private void ActiveSkill()
    {
        Instantiate(SkillPrefab, skillPos.position, Quaternion.identity);
    }

    private void Iskill()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            rb.velocity = new Vector2(transform.localScale.x * 15f, 0f);
            anim.SetTrigger("Iskill");
        }
    }
    /*private void OnDrawGizmosSelected()
    {
        if (meleeAttack01Pos == null) return;
        Gizmos.DrawWireSphere(meleeAttack01Pos.position, attackRange);
        Gizmos.DrawWireSphere(meleeAttack02Pos.position, attackRange);
    }*/
}
