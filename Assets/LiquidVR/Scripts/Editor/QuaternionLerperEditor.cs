using UnityEditor;
using UnityEngine;

namespace Liquid.Utils.Lerpers
{
    [CustomEditor(typeof(QuaternionLerper))]
    public class QuaternionLerperEditor : BaseLerperEditor
    {
        protected override void OnEnable()
        {
            GetEventsProperties(new []{"m_onLerpStart", "m_onLerpEnd", "m_onLerpStop", 
                "m_onLerpResume", "m_onLerpPause", "m_onLerpTick"});
        }
        
        public override void OnInspectorGUI()
        {
            var lerper = target as QuaternionLerper;
            
            lerper.LerpTime = EditorGUILayout.FloatField("Lerp Time", lerper.LerpTime);
            lerper.BeginValue = QuaternionField("Begin Value", lerper.BeginValue);
            lerper.EndValue = QuaternionField("End Value", lerper.EndValue);
            
            DrawEventsFoldout();
            
            serializedObject.ApplyModifiedProperties();
        }

        private Quaternion QuaternionField(string label, Quaternion quaternion)
        {
            var oldValue = new Vector4(quaternion.x, quaternion.y, quaternion.z, quaternion.w);
            var newValue = EditorGUILayout.Vector4Field(label, oldValue);
            return new Quaternion(newValue.x, newValue.y, newValue.z, newValue.w);
        }
    }
}