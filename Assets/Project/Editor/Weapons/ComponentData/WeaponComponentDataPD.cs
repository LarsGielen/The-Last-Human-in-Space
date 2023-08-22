using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Project.Weapons.Components;

namespace Project.Editor
{
    [CustomPropertyDrawer(typeof(WeaponComponentData), true)]
    public class WeaponComponentDataPD : PropertyDrawer
    {
        private int depth;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            depth = property.depth;

            var serializedProperty = property.FindPropertyRelative("name");
            bool searchChildren = serializedProperty.isExpanded;
            while (serializedProperty.Next(searchChildren))
            {
                if (serializedProperty.depth == depth) break;

                if (serializedProperty.isArray) searchChildren = false;
                else searchChildren = serializedProperty.isExpanded;

                EditorGUILayout.PropertyField(serializedProperty);
            }

            EditorGUILayout.EndVertical();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) => 0;
    }
}
