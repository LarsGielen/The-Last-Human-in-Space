using System;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Weapons.Components
{
    public class DetectDamageableComponent : WeaponComponent<DetectDamageableData>
    {
        private Vector3 origin;
        private Vector3 rotation;

        public Action<IDamageable[]> OnDetectedDamageable;

        protected override void Start()
        {
            base.Start();

            weapon.OnTriggerWeapon += DetectDamageble;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.OnTriggerWeapon -= DetectDamageble;
        }

        private void CalculateVariables()
        {
            switch (data.OriginType)
            {
                case DetectDamageableData.OriginTypeEnum.WeaponTransform:
                    Vector3 positionOffset = data.PositionOffset;
                    Vector3 positionOffsetRelative = transform.right * positionOffset.x + new Vector3(0f, positionOffset.y, 0f) + transform.forward * positionOffset.z;
                    origin = transform.position + positionOffsetRelative;
                    rotation = transform.rotation.eulerAngles + data.RotationOffset;
                    break;

                case DetectDamageableData.OriginTypeEnum.Mouse:
                    origin = GetMousePositionInWorld() + data.PositionOffset; // TODO: Will break if playing with controller!
                    rotation = data.RotationOffset;
                    break;

                default:
                    Debug.LogWarning($"{data.OriginType} is not defined in Switch statement");
                    break;
            }
        }

        private void DetectDamageble()
        {
            CalculateVariables();

            Collider[] damageableColliders = new Collider[0];

            if (data.DetectionType == DetectDamageableData.DetectionTypeEnum.Box)
                damageableColliders = Physics.OverlapBox(origin, data.Size, Quaternion.Euler(rotation), data.DamageableLayermask);

            else if (data.DetectionType == DetectDamageableData.DetectionTypeEnum.Sphere)
                damageableColliders = Physics.OverlapSphere(origin, data.Radius, data.DamageableLayermask);

            if (damageableColliders.Length == 0) return;

            List<IDamageable> damageables = new List<IDamageable>();
            foreach (Collider collider in damageableColliders)
            {
                if (collider.gameObject.TryGetComponent(out IDamageable damageable))
                    damageables.Add(damageable);
            }

            OnDetectedDamageable?.Invoke(damageables.ToArray());
        }

        private Vector3 GetMousePositionInWorld()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitData, 50, data.GroundLayerMask))
                return hitData.point;

            Debug.LogWarning("No ground found");
            return Vector3.zero;
        }

        private void OnDrawGizmosSelected()
        {
            if (data == null || !data.Debug) return;

            CalculateVariables();

            Gizmos.color = Color.red;

            Vector3 pos = origin;
            if (data.OriginType == DetectDamageableData.OriginTypeEnum.WeaponTransform)
            {
                Gizmos.matrix = Matrix4x4.TRS(origin, Quaternion.Euler(rotation), Vector3.one);
                pos = Vector3.zero;
            }

            if (data.DetectionType == DetectDamageableData.DetectionTypeEnum.Box)
                Gizmos.DrawWireCube(pos, data.Size);

            else if (data.DetectionType == DetectDamageableData.DetectionTypeEnum.Sphere)
                Gizmos.DrawWireSphere(pos, data.Radius);
        }
    }
}
