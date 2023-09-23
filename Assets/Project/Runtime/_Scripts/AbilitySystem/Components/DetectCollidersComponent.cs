using System;
using UnityEngine;

namespace Project.AbilitySystem.Components
{
    public class DetectCollidersComponent : AbilityComponent<DetectCollidersData>
    {
        private Vector3 origin;
        private Vector3 rotation;

        public Action<Collider[]> OnDetectedCollider;

        protected override void Enter()
        {
            base.Enter();

            DetectColliders();
        }

        private void CalculateVariables()
        {
            switch (data.OriginType)
            {
                case DetectCollidersData.OriginTypeEnum.WeaponTransform:
                    Vector3 positionOffset = data.PositionOffset;
                    Vector3 positionOffsetRelative = transform.right * positionOffset.x + new Vector3(0f, positionOffset.y, 0f) + transform.forward * positionOffset.z;
                    origin = transform.position + positionOffsetRelative;
                    rotation = transform.rotation.eulerAngles + data.RotationOffset;
                    break;

                case DetectCollidersData.OriginTypeEnum.Mouse:
                    origin = GetMousePositionInWorld() + data.PositionOffset; // TODO: Will break if playing with controller!
                    rotation = data.RotationOffset;
                    break;

                default:
                    Debug.LogWarning($"{data.OriginType} is not defined in Switch statement");
                    break;
            }
        }

        private void DetectColliders()
        {
            CalculateVariables();

            Collider[] colliders = new Collider[0];

            if (data.DetectionType == DetectCollidersData.DetectionTypeEnum.Box)
                colliders = Physics.OverlapBox(origin, data.Size, Quaternion.Euler(rotation), data.ColliderLayermask);

            else if (data.DetectionType == DetectCollidersData.DetectionTypeEnum.Sphere)
                colliders = Physics.OverlapSphere(origin, data.Radius, data.ColliderLayermask);

            if (colliders.Length == 0) return;

            OnDetectedCollider?.Invoke(colliders);
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
            if (data.OriginType == DetectCollidersData.OriginTypeEnum.WeaponTransform)
            {
                Gizmos.matrix = Matrix4x4.TRS(origin, Quaternion.Euler(rotation), Vector3.one);
                pos = Vector3.zero;
            }

            if (data.DetectionType == DetectCollidersData.DetectionTypeEnum.Box)
                Gizmos.DrawWireCube(pos, data.Size);

            else if (data.DetectionType == DetectCollidersData.DetectionTypeEnum.Sphere)
                Gizmos.DrawWireSphere(pos, data.Radius);
        }
    }
}
