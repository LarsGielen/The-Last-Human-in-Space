using UnityEngine;

namespace Project.AbilitySystem.Components
{
    public class ModelComponent : AbilityComponent<ModelData>
    {
        private MeshFilter meshFilter;
        private MeshRenderer meshRenderer;

        protected override void Start()
        {
            base.Start();

            meshFilter = ability.BaseObject.GetComponent<MeshFilter>();
            meshRenderer = ability.BaseObject.GetComponent<MeshRenderer>();
        }

        protected override void Enter()
        {
            base.Enter();

            meshFilter.mesh = data.WeaponMesh;
            meshRenderer.material = data.WeaponMaterial;
        }

        protected override void Exit()
        {
            base.Exit();

            meshFilter.mesh = null;
            meshRenderer.material = null;
        }
    }
}
