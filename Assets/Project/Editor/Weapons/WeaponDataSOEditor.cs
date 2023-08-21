using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

using Project.Weapons;
using Project.Weapons.Components;

namespace Project.Editor
{
    [CustomEditor(typeof(WeaponDataSO))]
    public class WeaponDataSOEditor : ExtendedEditor
    {
        private static List<Type> dataComponentTypes = new List<Type>();

        private WeaponDataSO dataSO;

        private void OnEnable() => dataSO = (WeaponDataSO)target;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            foreach (var componentType in dataComponentTypes)
            {
                if (GUILayout.Button(componentType.Name))
                {
                    var component = (WeaponComponentData)Activator.CreateInstance(componentType);
                    dataSO.AddData(component);
                }
            }
        }

        [DidReloadScripts]
        private static void OnRecompile()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var componentDataTypes = assemblies.SelectMany(assembly => assembly.GetTypes()).Where(
                type => type.IsSubclassOf(typeof(WeaponComponentData)) && !type.ContainsGenericParameters && type.IsClass
            );

            dataComponentTypes = componentDataTypes.ToList();
        }
    }
}