using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Liquid.Utils.Lerpers
{
    [CustomEditor(typeof(ColorLerper))]
    public class ColorLerperEditor : BaseLerperEditor
    {
        protected override void OnEnable()
        {
            GetEventsProperties(new []{"m_onLerpStart", "m_onLerpEnd", "m_onLerpStop", 
                "m_onLerpResume", "m_onLerpPause", "m_onLerpTick"});
        }
        
        public override void OnInspectorGUI()
        {
            var lerper = target as ColorLerper;
            
            lerper.LerpTime = EditorGUILayout.FloatField("Lerp Time", lerper.LerpTime);
            lerper.BeginValue = EditorGUILayout.ColorField("Begin Color", lerper.BeginValue);
            lerper.EndValue = EditorGUILayout.ColorField("End Color", lerper.EndValue);
            
            DrawEventsFoldout();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
