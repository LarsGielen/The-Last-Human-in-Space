using UnityEngine;

using Project.Entity;

namespace Project.Weapons.Components
{
    public class DamageComponent : WeaponComponent<DamageData>
    {
        private DetectCollidersComponent detectDamageableComponent;

        protected override void Start()
        {
            base.Start();

            if (!TryGetComponent(out detectDamageableComponent))
                Debug.LogWarning($"DamageComponent on weapon {gameObject.name} needs a DetectDamageableComponent!");
            else detectDamageableComponent.OnDetectedCollider += HandleDetectDamageable;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            detectDamageableComponent.OnDetectedCollider -= HandleDetectDamageable;
        }

        private void HandleDetectDamageable(Collider[] colliders)
        {
            foreach (Collider collider in colliders)
            {
                if (collider.TryGetComponent(out IDamageable damageable))
                    damageable.Damage(data.Damage);
            }
        }
    }
}
