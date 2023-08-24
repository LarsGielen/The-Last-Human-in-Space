using UnityEngine;

namespace Project.Weapons.Components
{
    public class ModelData : WeaponComponentData
    {
        public ModelData() => ComponentDependency = typeof(ModelComponent);

        [SerializeField] private Mesh weaponMesh;
        [SerializeField] private Material weaponMaterial;

        public Mesh WeaponMesh { get => weaponMesh; }
        public Material WeaponMaterial { get => weaponMaterial; }
    }
}