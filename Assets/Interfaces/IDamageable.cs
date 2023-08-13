using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float currentHealth { get; }
    
    void TakeDamage(float damageAmount);
    void GiveHealth(float healAmount);
    void Die();
}

