using UnityEngine;

namespace Project.AbilitySystem.Components
{
    public class DetectCollidersData : AbilityComponentData
    {
        public enum OriginTypeEnum { WeaponTransform, Mouse };
        public enum DetectionTypeEnum { Box, Sphere };

        [SerializeField] private OriginTypeEnum originType;
        [SerializeField] private LayerMask groundLayerMask;

        [SerializeField] private DetectionTypeEnum detectionType;
        [SerializeField] private Vector3 size;
        [SerializeField] private float radius;

        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private Vector3 rotationOffset;

        [SerializeField] private LayerMask colliderLayermask;

        [SerializeField] private bool debug;

        public OriginTypeEnum OriginType { get => originType; }
        public LayerMask GroundLayerMask { get => groundLayerMask; }
        
        public DetectionTypeEnum DetectionType { get => detectionType; }
        public Vector3 Size { get => size; }
        public float Radius { get => radius; }
        
        public Vector3 PositionOffset { get => positionOffset; }
        public Vector3 RotationOffset { get => rotationOffset; }
        
        public LayerMask ColliderLayermask { get => colliderLayermask; }
        
        public bool Debug { get => debug; }

        protected override void SetComponentDependency() => ComponentDependency = typeof(DetectCollidersComponent);
    }
}
