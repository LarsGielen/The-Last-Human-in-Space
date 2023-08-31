using UnityEngine;

namespace Project.AbilitySystem.Components
{
    public class SpawnPrefabData : AbilityComponentData
    {
        public enum OriginTypeEnum { Mouse }

        [SerializeField] private OriginTypeEnum originType;
        [SerializeField] private GameObject prefab;
        [SerializeField] private LayerMask layerMask;

        public OriginTypeEnum OriginType { get => originType; }
        public GameObject Prefab { get => prefab; }
        public LayerMask LayerMask { get => layerMask; }

        protected override void SetComponentDependency() => ComponentDependency = typeof(SpawnPrefabComponent);
    }
}
