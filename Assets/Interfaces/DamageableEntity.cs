using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageableEntity: MonoBehaviour
{
    protected float currentHealth;
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float startingHealth;

    public DamageableEntity(float maxHealth, float startingHealth)
    {
        this.maxHealth = maxHealth;
        this.startingHealth = startingHealth;
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
        //Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0) Die();
    }

    public void GiveHealth(float healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        //Debug.Log("Current Health: " + currentHealth);
    }

    protected abstract void Die();
}