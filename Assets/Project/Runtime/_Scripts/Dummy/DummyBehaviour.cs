using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class DummyBehaviour : MonoBehaviour, IHasHealth
    {
        public void Damage(float damageAmount)
        {
            Debug.Log($"Dummy took {damageAmount} damage");
        }

        public void Die()
        {
            
        }

        public void Heal(float healAmount)
        {
            
        }
    }
}
