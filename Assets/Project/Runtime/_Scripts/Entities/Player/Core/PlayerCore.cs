using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Player.Core
{
    public class PlayerCore : MonoBehaviour
    {
        public PlayerDataSO PlayerData { get => playerData ??= GetComponentInParent<Player>().PlayerData; }
        private PlayerDataSO playerData;

        [field: SerializeField] public Transform parentTransform { get; private set; }

        private readonly List<CoreComponent> coreComponents = new List<CoreComponent>();

        public void AddComonponent(CoreComponent comonponent)
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
