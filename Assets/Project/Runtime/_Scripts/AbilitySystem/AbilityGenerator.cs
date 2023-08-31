using System;
using UnityEngine;

using Project.AbilitySystem.Components;
using System.Collections.Generic;
using System.Linq;

namespace Project.AbilitySystem
{
    [RequireComponent(typeof(Ability))]
    public class AbilityGenerator : MonoBehaviour
    {
        [SerializeField] AbilityDataSO data;

        private Ability ability;

        private void Awake() => ability = GetComponent<Ability>();

        private void Start() => GenerateAbility(data);

        [ContextMenu("Force Generate")]
        private void ForceGenerate() => GenerateAbility(data);

        public void GenerateAbility(AbilityDataSO data)
        {
            ability.SetData(data);

            AbilityComponent[] attachedComponents = GetComponents<AbilityComponent>();
            Type[] componentDependencies = data.getAllDependecies();

            List<AbilityComponent> addedComponents = new List<AbilityComponent>();

            // Add needed dependencies
            foreach (var dependency in componentDependencies)
            {
                AbilityComponent componentToAdd = attachedComponents.FirstOrDefault(component => component.GetType() == dependency);
                
                if (componentToAdd == null)
                {
                    componentToAdd = (AbilityComponent)gameObject.AddComponent(dependency);
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
