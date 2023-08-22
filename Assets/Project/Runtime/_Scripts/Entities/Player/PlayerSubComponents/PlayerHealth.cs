using UnityEngine;
using System;

namespace Project
{
    public class PlayerHealth : MonoBehaviour, IDamageable, IHealable, IHasHealth
    {
        [SerializeField] private float maxHealth = 100f;
        HealthSystem healthSystem;

        public event Action<float> OnHealthChanged;

        private void Start()
        {
            healthSystem = new HealthSystem(maxHealth);
        }

        public void Damage(float damageAmount)
        {
            healthSystem.Damage(damageAmount);

            if (healthSystem.currentHealth <= 0) Die();
            else OnHealthChanged?.Invoke(healthSystem.currentHealth);
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

        public float GetMaxHealth(){ return maxHealth;}
    }
}