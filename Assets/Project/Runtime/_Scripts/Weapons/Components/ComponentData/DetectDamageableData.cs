using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Weapons.Components
{
    public class DetectDamageableData : WeaponComponentData
    {
        public DetectDamageableData() => ComponentDependency = typeof(DetectDamageableComponent);

        public enum OriginTypeEnum { WeaponTransform, Mouse };
        public enum DetectionTypeEnum { Box, Sphere };

        [SerializeField] private OriginTypeEnum originType;
        [SerializeField] private LayerMask groundLayerMask;

        [SerializeField] private DetectionTypeEnum detectionType;
        [SerializeField] private Vector3 size;
        [SerializeField] private float radius;

        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private Vector3 rotationOffset;

        [SerializeField] private LayerMask damageableLayermask;

        [SerializeField] private bool debug;

        public OriginTypeEnum OriginType { get => originType; }
        public LayerMask GroundLayerMask { get => groundLayerMask; }
        
        public DetectionTypeEnum DetectionType { get => detectionType; }
        public Vector3 Size { get => size; }
        public float Radius { get => radius; }
        
        public Vector3 PositionOffset { get => positionOffset; }
        public Vector3 RotationOffset { get => rotationOffset; }
        
        public LayerMask DamageableLayermask { get => damageableLayermask; }
        
        public bool Debug { get => debug; }
    }
}
