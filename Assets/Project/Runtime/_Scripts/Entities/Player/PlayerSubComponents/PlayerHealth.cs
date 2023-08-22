using UnityEngine;
using System;

namespace Project
{
    public class PlayerHealth : MonoBehaviour, IDamageable, IHealable, IHasHealth
    {
        [SerializeField] public float MaxHealth { get; set; }

        HealthSystem healthSystem;

        public event Action<float> OnHealthChanged;

        private void Start()
        {
            MaxHealth = 100f;
            healthSystem = new HealthSystem(MaxHealth);
        }

        public void Damage(float damageAmount)
        {
            healthSystem.Damage(damageAmount);
            OnHealthChanged?.Invoke(healthSystem.currentHealth);

            if (healthSystem.currentHealth <= 0) Die();
        }

        public void Heal(float healAmount)
        {
            healthSystem.Heal(healAmount);
            OnHealthChanged?.Invoke(healthSystem.currentHealth);
        }

        public void Die()
        {
            Debug.Log("The player is dead!");
        }

        public float GetCurrentHealth() { return healthSystem.currentHealth; }
    }
}