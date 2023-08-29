using UnityEngine;

namespace Project.Weapons.Components
{
    public class SpawnPrefabComponent : WeaponComponent<SpawnPrefabData>
    {
        protected override void Start()
        {
            base.Start();

            weapon.OnTriggerWeapon += SpawnPrefab;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            weapon.OnTriggerWeapon -= SpawnPrefab;
        }

        private void SpawnPrefab()
        {
            Instantiate(data.Prefab, GetPosition(), Quaternion.identity);
        }

        private Vector3 GetPosition()
        {
            switch (data.OriginType)
            {
                case SpawnPrefabData.OriginTypeEnum.Mouse:
                    return GetMousePositionInWorld();
                default:
                    Debug.LogWarning($"{data.OriginType} is not defined in Switch statement");
                    return Vector3.zero;
            }
        }

        private Vector3 GetMousePositionInWorld()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out var hitData, 50, data.LayerMask))
                return hitData.point;

            Debug.LogWarning("No ground found");
            return Vector3.zero;
        }
    }
}
