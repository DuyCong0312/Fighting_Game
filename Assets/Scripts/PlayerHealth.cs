using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    public float currentHealth;
    [SerializeField] private HealthBar healthBar;

    [SerializeField] private GameObject hit;
    [SerializeField] private Transform hitPos;

    private Animator anim;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            TakeDamage(5);
        }
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("getHurt");
        //doan duoi la day lui khi bi tan cong
        //this.transform.position = new Vector2(this.transform.position.x - 0.2f, this.transform.position.y);
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
        Instantiate(hit, hitPos.position, transform.rotation);
        if (currentHealth <= 0)
        {
            Debug.Log("0 health");
            //Die();
        }
    }

    public void ResetHealth()
    {
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Die()
    {
        this.gameObject.SetActive(false);
    }

    public void SetHealthBar(HealthBar bar)
    {
        healthBar = bar;
    }
}
