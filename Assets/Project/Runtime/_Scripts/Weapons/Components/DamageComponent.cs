using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Weapons.Components
{
    public class DamageComponent : WeaponComponent<DamageData>
    {
        private DetectDamageableComponent detectDamageableComponent;

        protected override void Start()
        {
            base.Start();

            if (!TryGetComponent(out detectDamageableComponent))
                Debug.LogWarning($"DamageComponent on weapon {gameObject.name} needs a DetectDamageableComponent!");
            else detectDamageableComponent.OnDetectedDamageable += HandleDetectDamageable;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            detectDamageableComponent.OnDetectedDamageable -= HandleDetectDamageable;
        }

        private void HandleDetectDamageable(IDamageable[] damageables)
        {
            foreach (IDamageable damageable in damageables)
            {
                damageable.Damage(data.Damage);
            }
        }
    }
}
