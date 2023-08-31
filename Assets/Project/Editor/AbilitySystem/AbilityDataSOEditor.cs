using System;
using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

using Project.AbilitySystem;
using Project.AbilitySystem.Components;

namespace Project.Editor
{
    [CustomEditor(typeof(AbilityDataSO))]
    public class AbilityDataSOEditor : ExtendedEditor
    {
        private static List<Type> dataComponentTypes = new List<Type>();

        private GUIStyle deleteButtonStyle;
        private bool showButtons = true;

        private AbilityDataSO dataSO;

        private void OnEnable()
        {
            dataSO = (AbilityDataSO)target;

            deleteButtonStyle = new GUIStyle(EditorStyles.miniButtonRight);
            deleteButtonStyle.normal.textColor = new Color(0.9f, 0.1f, 0.1f);
            deleteButtonStyle.onHover.textColor = new Color(1f, 0f, 0f);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            DrawField("weaponName");

            DrawComponents(serializedObject.FindProperty("componentDatas"), true);

            Apply();

            DrawButtons();
        }

        private void DrawComponents(SerializedProperty prop, bool drawChildren)
        {
            int index = 0;
            foreach (SerializedProperty p in prop)
            {
                EditorGUILayout.BeginHorizontal();
                p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName, EditorStyles.foldoutHeader);
                
                if (GUILayout.Button("Delete", deleteButtonStyle, GUILayout.Width(60)))
                    dataSO.RemoveData(index);
                
                EditorGUILayout.EndHorizontal();
                
                if (p.isExpanded)
                {
                    EditorGUI.indentLevel++;
                    EditorGUILayout.PropertyField(p);
                    EditorGUI.indentLevel--;
                }
                                
                index++;
            }
        }

        private void DrawButtons()
        {
            EditorGUILayout.Space(20);

            showButtons = EditorGUILayout.BeginFoldoutHeaderGroup(showButtons, "Add Component Buttons");

            if (showButtons)
            {
                foreach (var componentType in dataComponentTypes)
                {
                    if (GUILayout.Button(componentType.Name))
                    {
                        var component = (AbilityComponentData)Activator.CreateInstance(componentType);
                        dataSO.AddData(component);
                    }
                }
            }

            EditorGUILayout.EndFoldoutHeaderGroup();
        }

        [DidReloadScripts]
        private static void OnRecompile()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var componentDataTypes = assemblies.SelectMany(assembly => assembly.GetTypes()).Where(
                type => type.IsSubclassOf(typeof(AbilityComponentData)) && !type.ContainsGenericParameters && type.IsClass
            );

            dataComponentTypes = componentDataTypes.ToList();
        }
    }
}