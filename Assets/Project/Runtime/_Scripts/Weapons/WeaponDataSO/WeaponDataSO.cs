using System.Collections.Generic;
using UnityEngine;

using Project.Weapons.Components;
using System.Linq;

namespace Project.Weapons
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Weapons/WeaponData")]
    public class WeaponDataSO : ScriptableObject
    {
        [field: SerializeField] public string WeaponName {  get; private set; }

        [field: SerializeReference] public List<WeaponComponentData> componentDatas { get; private set; }

        public void AddData(WeaponComponentData data)
        {
            if (componentDatas.FirstOrDefault(t => t.GetType() == data.GetType()) != null) return;

            componentDatas.Add(data);
        }

        public T GetData<T>() where T : WeaponComponentData
        {
            return componentDatas.OfType<T>().FirstOrDefault();
        }
    }
}
