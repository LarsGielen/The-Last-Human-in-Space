using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasHealth
{    
    void Damage(float damageAmount);
    void Heal(float healAmount);
    void Die();
}

