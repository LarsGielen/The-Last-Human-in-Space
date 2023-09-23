using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Project.Entity.CoreSystem
{
    public class Core : MonoBehaviour
    {
        public EntityDataSO EntityData { get => entityData ??= GetComponentInParent<Player.Player>().PlayerData; }
        private EntityDataSO entityData;

        [field: SerializeField] public Transform parentTransform { get; private set; }

        private readonly List<CoreComponent> coreComponents = new List<CoreComponent>();

        public void AddComponent(CoreComponent comonponent)
        {
            if (!coreComponents.Contains(comonponent)) coreComponents.Add(comonponent);
        }

        public T GetCoreComponent<T>() where T : CoreComponent
        {
            var component = coreComponents.OfType<T>().FirstOrDefault();

            if (component != null) return component;
            component = GetComponentInChildren<T>();

            if (component != null) return component;
            Debug.LogWarning($"{typeof(T)} not found on {gameObject.name}");
            return null;
        }
    }
}
