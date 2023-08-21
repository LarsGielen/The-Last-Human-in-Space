using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Weapons.Components
{
    public class DetectDamageableComponent : WeaponComponent<DetectDamageableData>
    {
        private Vector3 origin;
        private Vector3 rotation;

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

            print($"{damageableColliders[0]}");
        }

        private Vector3 GetMousePositionInWorld()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitData;
            Physics.Raycast(ray, out hitData, 50, data.GroundLayerMask);

            return hitData.point;
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            weapon.OnTriggerWeapon += DetectDamageble;
        }

        protected override void OnDisable()
        {
            base.OnDisable();

            weapon.OnTriggerWeapon -= DetectDamageble;
        }

        private void OnDrawGizmosSelected()
        {
            if (data == null || !data.Debug) return;

            CalculateVariables();

            Gizmos.color = Color.red;
            Gizmos.matrix = Matrix4x4.TRS(origin, Quaternion.Euler(rotation), Vector3.one);

            if (data.DetectionType == DetectDamageableData.DetectionTypeEnum.Box)
                Gizmos.DrawWireCube(Vector3.zero, data.Size);

            else if (data.DetectionType == DetectDamageableData.DetectionTypeEnum.Sphere)
                Gizmos.DrawWireSphere(Vector3.zero, data.Radius);
        }
    }
}
