using UnityEngine;

namespace Project
{
    public class PlayerBehaviour : MonoBehaviour, IHasHealth
    {
        [SerializeField] private float maxHealth = 100f;

        HealthSystem healthSystem;

        private void Start()
        {
            healthSystem = new HealthSystem(maxHealth);
        }


        public void Damage(float damageAmount)
        {
            healthSystem.Damage(damageAmount);

            if (healthSystem.currentHealth <= 0) Die();
        }

        public void Heal(float healAmount)
        {
            healthSystem.Heal(healAmount);
        }

        public void Die()
        {
            Debug.Log("The player is dead!");
        }
    }
}