using UnityEngine;

using Project.Weapons.Components.data;

namespace Project.Weapons.Components
{
    public class WeaponModel : WeaponComponent
    {
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        private WeaponModelData data;

        protected override void Awake()
        {
            base.Awake();

            // TODO: Use when created weapon data!!!
            // meshFilter = weapon.WeaponBaseObject.GetComponent<MeshFilter>();
            // meshRenderer = weapon.WeaponBaseObject.GetComponent<MeshRenderer>();

            meshFilter = transform.Find("Base").GetComponent<MeshFilter>();
            meshRenderer = transform.Find("Base").GetComponent<MeshRenderer>();

            data = weapon.Data.GetData<WeaponModelData>();
        }

        protected override void HandleEnter()
        {
            base.HandleEnter();

            meshFilter.mesh = data.WeaponMesh;
            meshRenderer.material = data.WeaponMaterial;
        }

        protected override void HandleExit()
        {
            base.HandleExit();

            meshFilter.mesh = null;
            meshRenderer.material = null;
        }
    }
}
