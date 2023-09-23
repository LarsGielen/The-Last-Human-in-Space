using UnityEngine;

namespace Project.AbilitySystem.Components
{
    public class ModelData : AbilityComponentData
    {
        [SerializeField] private Mesh weaponMesh;
        [SerializeField] private Material weaponMaterial;

        public Mesh WeaponMesh { get => weaponMesh; }
        public Material WeaponMaterial { get => weaponMaterial; }

        protected override void SetComponentDependency() => ComponentDependency = typeof(ModelComponent);
    }
}
