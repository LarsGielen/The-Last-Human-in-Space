using UnityEngine;

namespace Project.Weapons.Components
{
    public class ModelComponent : WeaponComponent<ModelData>
    {
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        protected override void Start()
        {
            base.Start();

            meshFilter = weapon.WeaponBaseObject.GetComponent<MeshFilter>();
            meshRenderer = weapon.WeaponBaseObject.GetComponent<MeshRenderer>();
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
