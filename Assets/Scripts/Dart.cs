using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart: MonoBehaviour
{
    protected Animator anim;
    protected Rigidbody2D rb;
    [SerializeField] protected float attackDamage = 5f;
    [SerializeField] protected float speed = 10f;
    [SerializeField] protected GameObject effect;

    protected virtual void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage);
            Destroy(this.gameObject);
            WhenHit();
        }
        
        Debug.Log(collision.name);
    }

    protected virtual void WhenHit()
    {
        Instantiate(effect, this.transform.position, transform.rotation * Quaternion.Euler(0, 0, 45));
    }
}
