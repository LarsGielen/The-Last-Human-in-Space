using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Project.Weapons.Components;

namespace Project.Editor
{
    [CustomPropertyDrawer(typeof(DetectCollidersData), false)]
    public class DetectDamageableDataPD : WeaponComponentDataPD
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUILayout.BeginVertical(EditorStyles.helpBox);

            // Origin Type
            SerializedProperty originType = property.FindPropertyRelative("originType");
            EditorGUILayout.PropertyField(originType);

            if (originType.intValue == ((int)DetectCollidersData.OriginTypeEnum.Mouse))
                EditorGUILayout.PropertyField(property.FindPropertyRelative("groundLayerMask"));

            EditorGUILayout.Space();

            // Detection Type
            SerializedProperty detectionType = property.FindPropertyRelative("detectionType");
            EditorGUILayout.PropertyField(detectionType);

            if (detectionType.intValue == ((int)DetectCollidersData.DetectionTypeEnum.Box))
                EditorGUILayout.PropertyField(property.FindPropertyRelative("size"));
            else if (detectionType.intValue == ((int)DetectCollidersData.DetectionTypeEnum.Sphere))
                EditorGUILayout.PropertyField(property.FindPropertyRelative("radius"));

            EditorGUILayout.Space();

            // Offsets
            EditorGUILayout.LabelField("Offsets", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(property.FindPropertyRelative("positionOffset"));
            EditorGUILayout.PropertyField(property.FindPropertyRelative("rotationOffset"));

            EditorGUILayout.Space();

            // Rest
            EditorGUILayout.PropertyField(property.FindPropertyRelative("damageableLayermask"));

            SerializedProperty debug = property.FindPropertyRelative("debug");
            EditorGUILayout.PropertyField(debug);

            if (debug.boolValue)
                EditorGUILayout.HelpBox("Select the weapon in the scene in play mode to see gizmos", MessageType.Info);

            EditorGUILayout.EndVertical();
        }
    }
}
