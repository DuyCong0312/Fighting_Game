using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    public float currentHealth;
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private string animationHeavyHurtName;

    private Animator anim;
    private float damageThreshold = 20f;  
    private float damageTimeLimit = 2f; 
    private float accumulatedDamage = 0f;
    private float timeSinceLastDamage = 0f;
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
        if (timeSinceLastDamage < damageTimeLimit)
        {
            timeSinceLastDamage += Time.deltaTime;
        }
        else
        {
            accumulatedDamage = 0;
        }
        PlayHeavyHurt();
    }
    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        anim.SetTrigger("getHurt");
        accumulatedDamage += damage;
        timeSinceLastDamage = 0f;
        //doan duoi la day lui khi bi tan cong
        //this.transform.position = new Vector2(this.transform.position.x - 0.2f, this.transform.position.y);
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
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

    private void PlayHeavyHurt()
    {
        if(accumulatedDamage >= damageThreshold && timeSinceLastDamage < damageTimeLimit)
        {
            anim.Play(animationHeavyHurtName);
            accumulatedDamage = 0f;
        }
    }
}
