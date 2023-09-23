using UnityEngine;

namespace Project.AbilitySystem.Components
{
    public class SpawnPrefabComponent : AbilityComponent<SpawnPrefabData>
    {
        protected override void Enter()
        {
            SpawnPrefab();
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
