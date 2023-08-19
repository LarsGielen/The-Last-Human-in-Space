using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Project.Editor
{
    public class ExtendedEditor : UnityEditor.Editor
    {

        protected SerializedProperty DrawField(string propertyName)
        {
            SerializedProperty serializedProperty = serializedObject.FindProperty(propertyName);
            EditorGUILayout.PropertyField(serializedProperty);
            return serializedProperty;
        }

        protected void Apply()
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
