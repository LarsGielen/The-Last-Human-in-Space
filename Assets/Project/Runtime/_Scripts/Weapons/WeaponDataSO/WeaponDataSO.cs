using System.Collections.Generic;
using UnityEngine;

using Project.Weapons.Components.data;
using System.Linq;

namespace Project.Weapons
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Weapons/WeaponData")]
    public class WeaponDataSO : ScriptableObject
    {
        [field: SerializeReference] public List<ComponentData> componentDatas { get; private set; }

        public T GetData<T>() where T : ComponentData
        {
            return componentDatas.OfType<T>().FirstOrDefault();
        }

        [ContextMenu("Add Model Data")]
        private void AddModelComponent() => componentDatas.Add(new WeaponModelData());
    }
}
