using UnityEngine;

namespace Project.Weapons.Components
{
    public class WeaponModelData : WeaponComponentData
    {
        [SerializeField] private Mesh weaponMesh;
        [SerializeField] private Material weaponMaterial;

        public Mesh WeaponMesh { get; private set; }
        public Material WeaponMaterial { get; private set; }
    }
}
