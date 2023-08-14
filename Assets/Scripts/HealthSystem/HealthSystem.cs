using System;

public class HealthSystem
{
    public float maxHealth { get; private set; }
    public float currentHealth { get; private set; }

    public HealthSystem(float maxHealth)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
    }

    public void Damage(float damageAmount)
    {
        currentHealth = Math.Max(currentHealth - damageAmount, 0);
    }

    public void Heal(float healAmount)
    {
        currentHealth = Math.Min(currentHealth + healAmount, maxHealth);
    }
}
