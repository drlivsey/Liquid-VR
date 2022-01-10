using UnityEngine;
using UnityEditor;
using Liquid.CustomInspector.Utils;

namespace Liquid.Utils.Lerpers
{
    [CustomEditor(typeof(BaseLerper))]
    public class BaseLerperEditor : Editor
    {
        private bool _showEvents = false;
        private SerializedProperty[] _eventsProperties = null;
        
        protected virtual void OnEnable()
        {
            GetEventsProperties(new []{"m_onLerpStart", "m_onLerpEnd", "m_onLerpStop", 
                "m_onLerpResume", "m_onLerpPause"});
        }

        public override void OnInspectorGUI()
        {
            var lerper = target as BaseLerper;
            lerper.LerpTime = EditorGUILayout.FloatField("Lerp Time", lerper.LerpTime);
            
            DrawEventsFoldout();
            
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