using Liquid.Core;
using UnityEditor;
using Liquid.CustomInspector.Utils;
using UnityEngine;

namespace Liquid.Subsystems.Updating
{
    [CustomEditor(typeof(BaseUpdatableComponent), true)]
    public class BaseUpdatableComponentEditor : Editor
    {
        private bool _showEvents = false;
        private SerializedProperty[] _eventsProperties = null;

        private void OnEnable()
        {
            GetEventsProperties();
        }

        public override void OnInspectorGUI()
        {
            DrawProperties();
            DrawEventsFoldout();

            serializedObject.ApplyModifiedProperties();
        }

        private void DrawProperties()
        {
            var component = target as BaseUpdatableComponent;
            
            component.UpdatingState = (UpdatingState)EditorGUILayout.EnumPopup("Updating State", component.UpdatingState);

            if (component.UpdatingState == UpdatingState.UpdateByTime)
            {
                component.UpdateFrequency = EditorGUILayout.FloatField("Update Frequency", component.UpdateFrequency);
                component.UpdateInRealtime = EditorGUILayout.Toggle("Update In Realtime", component.UpdateInRealtime);
            }
        }

        private void GetEventsProperties()
        {
            if (serializedObject.TryGetPropertiesByName(new[] {"m_onUpdate", "m_onRegister", "m_onUnregister"},
                out _eventsProperties) == false)
            {
                Debug.LogError("There are no property with some name from list!");
            }
        }

        private void DrawEventsFoldout()
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