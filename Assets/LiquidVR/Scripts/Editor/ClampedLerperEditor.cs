using UnityEditor;

namespace Liquid.Utils.Lerpers
{
    [CustomEditor(typeof(ClampedLerper))]
    public class ClampedLerperEditor : BaseLerperEditor
    {
        protected override void OnEnable()
        {
            GetEventsProperties(new []{"m_onLerpStart", "m_onLerpEnd", "m_onLerpStop", 
                "m_onLerpResume", "m_onLerpPause", "m_onLerpTick"});
        }
        
        public override void OnInspectorGUI()
        {
            var lerper = target as ClampedLerper;
            
            lerper.LerpTime = EditorGUILayout.FloatField("Lerp Time", lerper.LerpTime);
            
            DrawEventsFoldout();
            
            serializedObject.ApplyModifiedProperties();
        }
    }
}
