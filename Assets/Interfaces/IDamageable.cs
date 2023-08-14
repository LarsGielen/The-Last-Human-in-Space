using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    float currentHealth { get; }
    
    void Damage(float damageAmount);
    void Heal(float healAmount);
    void Die();
}

