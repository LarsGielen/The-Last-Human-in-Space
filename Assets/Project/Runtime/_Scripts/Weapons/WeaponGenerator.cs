using System;
using UnityEngine;

using Project.Weapons.Components;
using System.Collections.Generic;
using System.Linq;

namespace Project.Weapons
{
    [RequireComponent(typeof(Weapon))]
    public class WeaponGenerator : MonoBehaviour
    {
        [SerializeField] WeaponDataSO data;

        private Weapon weapon;

        private void Awake() => weapon = GetComponent<Weapon>();

        private void Start() => GenerateWeapon(data);

        [ContextMenu("Force Generate")]
        private void ForceGenerate() => GenerateWeapon(data);

        public void GenerateWeapon(WeaponDataSO data)
        {
            weapon.SetData(data);

            WeaponComponent[] attachedComponents = GetComponents<WeaponComponent>();
            Type[] componentDependencies = data.getAllDependecies();

            List<WeaponComponent> addedComponents = new List<WeaponComponent>();

            // Add needed dependencies
            foreach (var dependency in componentDependencies)
            {
                WeaponComponent componentToAdd = attachedComponents.FirstOrDefault(component => component.GetType() == dependency);
                
                if (componentToAdd == null)
                {
                    componentToAdd = (WeaponComponent)gameObject.AddComponent(dependency);
                }

                componentToAdd.Init();
                addedComponents.Add(componentToAdd);
            }

            // Remove unneeded components
            var componentsToRemove = attachedComponents.Except(addedComponents);
            foreach (var component in componentsToRemove) Destroy(component);
        }
    }
}
