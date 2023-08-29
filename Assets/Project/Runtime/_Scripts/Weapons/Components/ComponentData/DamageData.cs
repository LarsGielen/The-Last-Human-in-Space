using UnityEngine;

namespace Project.Weapons.Components
{
    public class DamageData : WeaponComponentData
    {
        [SerializeField] private float damage;

        public float Damage { get => damage; }

        protected override void SetComponentDependency() => ComponentDependency = typeof(DamageComponent);
    }
}