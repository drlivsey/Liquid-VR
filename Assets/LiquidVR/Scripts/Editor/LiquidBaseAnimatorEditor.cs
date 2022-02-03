using UnityEngine;
using UnityEditor;

namespace Liquid.Interactables
{
    [CustomEditor(typeof(LiquidBaseAnimator), true)]
    public class LiquidBaseAnimatorEditor : Editor 
    {
        public override void OnInspectorGUI() 
        {
            var animator = target as LiquidBaseAnimator;
            EditorGUILayout.HelpBox($"Begin position: {animator.StartPoint}\n"
                                            + $"End position: {animator.EndPoint}", MessageType.Info);
            base.OnInspectorGUI();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Set Begin Position"))
            {
                animator.SetStartPoint();
            }
            if (GUILayout.Button("Set End Position"))
            {
                animator.SetEndPoint();
            }
            GUILayout.EndHorizontal();
            serializedObject.ApplyModifiedProperties();
        }
    }
}