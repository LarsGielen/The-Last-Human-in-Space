using UnityEngine;

namespace Project.Weapons.Components
{
    public class WeaponModelData : WeaponComponentData
    {
        [field: SerializeField] public Mesh WeaponMesh { get; private set; }
        [field: SerializeField] public Material WeaponMaterial { get; private set; }
    }
}
