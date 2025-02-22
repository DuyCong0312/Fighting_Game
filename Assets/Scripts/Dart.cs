using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart: MonoBehaviour
{
    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject effect;

    private void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(attackDamage);
            Destroy(this.gameObject);
            Instantiate(effect, this.transform.position, transform.rotation * Quaternion.Euler(0, 0, 45));
        }
        
        Debug.Log(collision.name);
    }
}
