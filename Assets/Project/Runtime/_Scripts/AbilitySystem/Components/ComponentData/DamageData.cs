using UnityEngine;

namespace Project.AbilitySystem.Components
{
    public class DamageData : AbilityComponentData
    {
        [SerializeField] private float damage;

        public float Damage { get => damage; }

        protected override void SetComponentDependency() => ComponentDependency = typeof(DamageComponent);
    }
}