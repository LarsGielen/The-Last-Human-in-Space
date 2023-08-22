using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Weapons.Components
{
    public class DamageComponent : WeaponComponent<DamageData>
    {
        private DetectDamageableComponent detectDamageableComponent;

        protected override void Awake()
        {
            base.Awake();

            if (!TryGetComponent(out detectDamageableComponent))
                Debug.LogWarning($"DamageComponent on weapon {gameObject.name} needs a DetectDamageableComponent!");
        }

        private void HandleDetectDamageable(IDamageable[] damageables)
        {
            foreach (IDamageable damageable in damageables)
            {
                damageable.Damage(data.Damage);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            detectDamageableComponent.OnDetectedDamageable += HandleDetectDamageable;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            detectDamageableComponent.OnDetectedDamageable -= HandleDetectDamageable;
        }
    }
}
