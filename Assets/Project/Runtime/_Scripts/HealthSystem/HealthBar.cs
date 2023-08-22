using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Project
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        private Slider healthBar;
        [SerializeField] private GameObject objectWithHealth;
        private IHasHealth hasHealth;

        void Start()
        {
            healthBar = GetComponent<Slider>();

            if (objectWithHealth.TryGetComponent<IHasHealth>(out hasHealth)){
                hasHealth.OnHealthChanged += UpdateHealthBar;
            }
            else {Debug.Log("No IHasHealth found on GameObject");}
        }

        private void UpdateHealthBar(float newHealth)
        {
            // Debug.Log(newHealth);
            healthBar.value = newHealth / hasHealth.GetMaxHealth() ;
        }
    }
}
