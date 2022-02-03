using UnityEditor;

namespace Liquid.Utils.Lerpers
{
    [CustomEditor(typeof(Vector3Lerper))]
    public class Vector3LerperEditor : BaseLerperEditor
    {
        protected override void OnEnable()
        {
            GetEventsProperties(new []{"m_onLerpStart", "m_onLerpEnd", "m_onLerpStop", 
                "m_onLerpResume", "m_onLerpPause", "m_onLerpTick"});
        }
        
        public override void OnInspectorGUI()
        {
            var lerper = target as Vector3Lerper;
            
            lerper.LerpTime = EditorGUILayout.FloatField("Lerp Time", lerper.LerpTime);
            lerper.BeginValue = EditorGUILayout.Vector3Field("Begin Value", lerper.BeginValue);
            lerper.EndValue = EditorGUILayout.Vector3Field("End Value", lerper.EndValue);
            
            DrawEventsFoldout();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}