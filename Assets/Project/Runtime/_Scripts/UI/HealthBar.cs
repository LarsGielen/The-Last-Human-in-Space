using UnityEngine;
using UnityEngine.UI;

using Project.Entity;

namespace Project.UI
{
    [RequireComponent(typeof(Slider))]
    public class HealthBar : MonoBehaviour
    {
        private Slider healthBar;
        [SerializeField] private GameObject objectWithHealth;
        private IHasHealth hasHealth;

        void OnEnable()
        {
            healthBar = GetComponent<Slider>();

            if (objectWithHealth.TryGetComponent<IHasHealth>(out hasHealth)){
                hasHealth.OnHealthChanged += UpdateHealthBar;
            }
            else {Debug.LogWarning("No IHasHealth found on GameObject");}
        }

        private void UpdateHealthBar(float newHealth)
        {
            Debug.Log(newHealth / hasHealth.MaxHealth);
            healthBar.value = newHealth / hasHealth.MaxHealth;
        }

        private void OnDisable()
        {
            hasHealth.OnHealthChanged -= UpdateHealthBar;
        }
    }
}
