using UnityEditor;
using UnityEngine;

using Project.AbilitySystem.Components;

namespace Project.Editor
{
    [CustomPropertyDrawer(typeof(MovementData), true)]
    public class MovementComponentDataPD : AbilityComponentDataPD
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.PropertyField(property.FindPropertyRelative("moveType"));

            EditorGUILayout.Space();

            SerializedProperty directionType = property.FindPropertyRelative("directionType");
            EditorGUILayout.PropertyField(directionType);               

            switch (directionType.intValue)
            {
                case (int)MovementData.DirectionEnum.World:
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("direction"));
                    break;
                case (int)MovementData.DirectionEnum.Character:
                    EditorGUILayout.PropertyField(property.FindPropertyRelative("angle"));
                    break;
                default:
                    break;
            }

            EditorGUILayout.PropertyField(property.FindPropertyRelative("value"));

            EditorGUILayout.Space();
        }
    }
}
