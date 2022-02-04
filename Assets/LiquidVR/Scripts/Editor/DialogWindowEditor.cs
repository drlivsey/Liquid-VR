using UnityEditor;

namespace Liquid.UI
{
    [CustomEditor(typeof(DialogWindow))]
    public class DialogWindowEditor : Editor
    {
        private SerializedProperty _animator = null;

        private void OnEnable() 
        {
            _animator = serializedObject.FindProperty("m_dialogWindowAnimator");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            var dialogWindow = target as DialogWindow;

            if (dialogWindow.AnimatePopup)
            {
                EditorGUILayout.PropertyField(_animator);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
}