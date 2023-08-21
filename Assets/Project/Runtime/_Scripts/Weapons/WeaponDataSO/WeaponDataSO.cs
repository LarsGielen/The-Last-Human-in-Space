using System.Collections.Generic;
using UnityEngine;

using Project.Weapons.Components;
using System.Linq;

namespace Project.Weapons
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Weapons/WeaponData")]
    public class WeaponDataSO : ScriptableObject
    {
        [field: SerializeReference] public List<ComponentData> componentDatas { get; private set; }

        public void AddData(ComponentData data)
        {
            if (componentDatas.FirstOrDefault(t => t.GetType() == data.GetType()) != null) return;

            componentDatas.Add(data);
        }

        public T GetData<T>() where T : ComponentData
        {
            return componentDatas.OfType<T>().FirstOrDefault();
        }
    }
}
