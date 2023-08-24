using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Project.Editor
{
    public class ExtendedEditor : UnityEditor.Editor
    {
        protected void DrawProperties(SerializedProperty prop, bool drawChildren)
        {
            string lastPropPath = string.Empty;
            foreach (SerializedProperty p in prop)
            {
                if (p.isArray && p.propertyType == SerializedPropertyType.Generic)
                {
                    EditorGUILayout.BeginVertical();
                    p.isExpanded = EditorGUILayout.Foldout(p.isExpanded, p.displayName);
                    EditorGUILayout.EndVertical();

                    if (p.isExpanded)
                    {
                        EditorGUI.indentLevel++;
                        DrawProperties(p, drawChildren);
                        EditorGUI.indentLevel--;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(lastPropPath) && p.propertyPath.Contains(lastPropPath)) continue;
                    
                    lastPropPath = p.propertyPath;
                    EditorGUILayout.PropertyField(p, drawChildren);
                }
            }
        }

        protected void DrawField(string propertyName)
        {
            SerializedProperty serializedProperty = serializedObject.FindProperty(propertyName);
            EditorGUILayout.PropertyField(serializedProperty);
        }

        protected void Apply()
        {
            serializedObject.ApplyModifiedProperties();
        }
    }
}
