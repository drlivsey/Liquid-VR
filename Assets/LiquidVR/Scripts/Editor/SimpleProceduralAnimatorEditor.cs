using UnityEngine;
using UnityEditor;
using Liquid.CustomInspector.Utils;

namespace Liquid.Utils
{
    [CustomEditor(typeof(SimpleProceduralAnimator), true)]
    public class SimpleProceduralAnimatorEditor : Editor 
    {
        private bool _showEvents = false;
        private SerializedProperty[] _eventsProperties = null;
        
        protected virtual void OnEnable()
        {
            GetEventsProperties(new []{"m_onStart", "m_onPause", "m_onResume", 
                "m_onStop", "m_onAnimationBegin", "m_onAnimationEnd"});
        }
        
        public override void OnInspectorGUI() 
        {
            base.OnInspectorGUI();
            var animator = target as SimpleProceduralAnimator;
            DrawEventsFoldout();
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
        
        protected void GetEventsProperties(string[] names)
        {
            if (serializedObject.TryGetPropertiesByName(names, out _eventsProperties) == false)
            {
                Debug.LogError("There are no property with some name from list!");
            }
        }

        protected void DrawEventsFoldout()
        {
            _showEvents = EditorGUILayout.BeginFoldoutHeaderGroup(_showEvents, "Events");
            if (_showEvents)
            {
                EditorUtils.DrawPropertiesFields(_eventsProperties, true);
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
        }
    }
}