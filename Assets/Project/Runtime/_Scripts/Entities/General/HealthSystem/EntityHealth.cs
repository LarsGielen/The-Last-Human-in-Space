using System;
using UnityEngine;

namespace Project.Entity
{
    public class EntityHealth : MonoBehaviour, IDamageable, IHealable, IHasHealth
    {
        [SerializeField] public float MaxHealth { get; private set; }

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

        [ContextMenu("Take 50 Damage")]
        void TakeDamage()
        {
            Damage(50f);
            Debug.Log("Current health of " + gameObject.name + " is " + healthSystem.currentHealth);
        }

    }
}