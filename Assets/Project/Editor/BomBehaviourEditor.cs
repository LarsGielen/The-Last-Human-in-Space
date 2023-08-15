using UnityEngine;
using UnityEditor;

namespace Project.Editor
{
    [CustomEditor(typeof(Explosive))]
    public class BomBehaviourEditor : ExtendedEditor
    {

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            Explosive targetObject = (Explosive)target;

            if (GUILayout.Button("Detonate"))
            {
                targetObject.Trigger();
            }
        }

        private void OnSceneGUI()
        {
            float radius = serializedObject.FindProperty("radius").floatValue;

            Explosive targetObject = (Explosive)target;

            Handles.color = Color.red;
            Handles.DrawWireDisc(targetObject.transform.position, Vector3.up, radius);
        }
    }
}
