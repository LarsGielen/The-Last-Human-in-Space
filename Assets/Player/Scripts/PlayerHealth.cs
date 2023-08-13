using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [Header("Player health settings")]
    [SerializeField] private float maxHealth = 100f;
    [SerializeField] private float startingHealth = 100f;

    public float currentHealth { get; private set; }

    //Om te testen:
    private void Update() {
        TakeDamage(0.5f);
        GiveHealth(0.4f);
    }
    
    private void Start()
    {
        currentHealth = startingHealth;
    }

    public void TakeDamage(float damageAmount)
    {
        currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
        Debug.Log("Current Health: " + currentHealth);

        if (currentHealth <= 0) Die();
    }

    public void GiveHealth(float healAmount)
    {
        currentHealth = Mathf.Min(currentHealth + healAmount, maxHealth);
        Debug.Log("Current Health: " + currentHealth);
    }

    public void Die()
    {
        Debug.Log("The player is dead!");
    }
}
