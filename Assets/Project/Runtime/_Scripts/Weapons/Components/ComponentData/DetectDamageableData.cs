using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Weapons.Components
{
    public class DetectDamageableData : WeaponComponentData
    {
        public enum OriginTypeEnum { WeaponTransform, Mouse };
        public enum DetectionTypeEnum { Box, Sphere };

        [field: SerializeField] public OriginTypeEnum OriginType { get; private set; }
        [field: SerializeField] public LayerMask GroundLayerMask { get; private set; }

        [field: SerializeField] public DetectionTypeEnum DetectionType { get; private set; }
        [field: SerializeField] public float Radius { get; private set; }
        [field: SerializeField] public Vector3 Size { get; private set; }

        [field: SerializeField] public Vector3 PositionOffset { get; private set; }
        [field: SerializeField] public Vector3 RotationOffset { get; private set; }

        [field: SerializeField] public LayerMask DamageableLayermask { get; private set; }

        [field: SerializeField] public bool Debug { get; private set; }
    }
}
