using UnityEditor;
using UnityEngine;
using Liquid.CustomInspector.Utils;

namespace Liquid.Utils.Lerpers
{
    [CustomEditor(typeof(FloatLerper))]
    public class FloatLerperEditor : BaseLerperEditor
    {
        protected override void OnEnable()
        {
            GetEventsProperties(new []{"m_onLerpStart", "m_onLerpEnd", "m_onLerpStop", 
                "m_onLerpResume", "m_onLerpPause", "m_onLerpTick"});
        }
        
        public override void OnInspectorGUI()
        {
            var lerper = target as FloatLerper;
            
            lerper.LerpTime = EditorGUILayout.FloatField("Lerp Time", lerper.LerpTime);
            lerper.BeginValue = EditorGUILayout.FloatField("Begin Value", lerper.BeginValue);
            lerper.EndValue = EditorGUILayout.FloatField("End Value", lerper.EndValue);
            
            DrawEventsFoldout();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}