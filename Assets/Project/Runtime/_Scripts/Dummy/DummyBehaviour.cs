using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project
{
    public class DummyBehaviour : MonoBehaviour, IDamageable
    {
        public void Damage(float damageAmount)
        {
            Debug.Log($"Dummy took {damageAmount} damage");
        }
    }
}
