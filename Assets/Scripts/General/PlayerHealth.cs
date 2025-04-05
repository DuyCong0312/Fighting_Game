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
    private KnockBack knockBack;
    private PlayerState playerState;
    private float damageThreshold = 20f;  
    private float damageTimeLimit = 2f; 
    private float accumulatedDamage = 0f;
    private float timeSinceLastDamage = 0f;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        knockBack = GetComponentInChildren<KnockBack>();
        playerState = GetComponentInChildren<PlayerState>();
        currentHealth = maxHealth;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T)) 
        {
            TakeDamage(5, new Vector2(1,1));
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
    public void TakeDamage(float damage, Vector2 direction)
    {
        if (playerState.isDefending)
        {
            damage = 2f;
            currentHealth -= damage;
            knockBack.KnockBackAction(direction / 2f);
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
        else
        {
            currentHealth -= damage;
            anim.SetTrigger("getHurt");
            playerState.isGettingHurt = true;
            knockBack.KnockBackAction(direction);
            accumulatedDamage += damage;
            timeSinceLastDamage = 0f;
            healthBar.UpdateHealthBar(currentHealth, maxHealth);
        }
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
            playerState.isGettingHurt = true;
            accumulatedDamage = 0f;
        }
    }
}
