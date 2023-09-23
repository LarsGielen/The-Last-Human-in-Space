using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

using Project.AbilitySystem.Components;

namespace Project.AbilitySystem
{
    [CreateAssetMenu(fileName = "newAbilityData", menuName = "Data/AbilityData")]
    public class AbilityDataSO : ScriptableObject
    {
        [SerializeField] private string weaponName;
        [SerializeReference] private List<AbilityComponentData> componentDatas;

        // properties
        public string WeaponName { get => weaponName; }
        public List<AbilityComponentData> ComponentDatas { get => componentDatas; }

        // Methods
        public Type[] getAllDependecies() => ComponentDatas.Select(component => component.ComponentDependency).ToArray();

        public T GetData<T>() where T : AbilityComponentData => ComponentDatas.OfType<T>().FirstOrDefault();

        public void AddData(AbilityComponentData data)
        {
            if (ComponentDatas.FirstOrDefault(t => t.GetType() == data.GetType()) != null) return;

            ComponentDatas.Add(data);
        }

        public void RemoveData(int index) => componentDatas.RemoveAt(index);
    }
}
