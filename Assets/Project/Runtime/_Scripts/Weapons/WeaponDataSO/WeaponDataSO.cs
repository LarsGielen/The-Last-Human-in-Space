using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using Project.Weapons.Components;

namespace Project.Weapons
{
    [CreateAssetMenu(fileName = "newWeaponData", menuName = "Data/WeaponData")]
    public class WeaponDataSO : ScriptableObject
    {
        [SerializeField] private string weaponName;
        [SerializeReference] private List<WeaponComponentData> componentDatas;

        // properties
        public string WeaponName { get => weaponName; }
        public List<WeaponComponentData> ComponentDatas { get => componentDatas; }

        // Methods
        public Type[] getAllDependecies() => ComponentDatas.Select(component => component.ComponentDependency).ToArray();

        public T GetData<T>() where T : WeaponComponentData => ComponentDatas.OfType<T>().FirstOrDefault();

        public void AddData(WeaponComponentData data)
        {
            if (ComponentDatas.FirstOrDefault(t => t.GetType() == data.GetType()) != null) return;

            ComponentDatas.Add(data);
        }

        public void RemoveData(int index) => componentDatas.RemoveAt(index);
    }
}
