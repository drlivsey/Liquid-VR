using UnityEngine;
using UnityEditor;

namespace Liquid.Utils
{
    [CustomEditor(typeof(SimpleProceduralAnimator), true)]
    public class SimpleProceduralAnimatorEditor : Editor 
    {
        public override void OnInspectorGUI() 
        {
            base.OnInspectorGUI();
            var animator = target as SimpleProceduralAnimator;
            EditorGUILayout.Separator();
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Set Begin Value"))
            {
                animator.SetBeginValues();
            }
            if (GUILayout.Button("Set End Value"))
            {
                animator.SetEndValues();
            }
            EditorGUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
        }
    }
}