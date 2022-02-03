using System.IO;
using UnityEditor;

namespace Liquid.Utils
{
    [CustomEditor(typeof(ScenesLoader), true)]
    public class ScenesLoaderEditor : Editor
    {
        private bool _showEvents = false;
        public override void OnInspectorGUI()
        {
            var loader = target as ScenesLoader;
            var scenesNames = GetScenesNames();
            var index = GetIndexOfScene(scenesNames, loader.SceneName);

            index = EditorGUILayout.Popup("Scene Name", index, scenesNames);

            EditorGUILayout.PropertyField(serializedObject.FindProperty("m_loadingMode"));

            _showEvents = EditorGUILayout.BeginFoldoutHeaderGroup(_showEvents, "Events");
            if (_showEvents)
            {
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_onLoadingBegin"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("m_onLoadingComplete"));
            }
            EditorGUILayout.EndFoldoutHeaderGroup();

            if (index >= 0 && index < scenesNames.Length)
            {
                loader.SceneName = scenesNames[index];
            }

            serializedObject.ApplyModifiedProperties();
        }

        private string[] GetScenesNames()
        {
            var buildScenes = EditorBuildSettings.scenes;
            var scenesNames = new string[buildScenes.Length];

            for (var i = 0; i < scenesNames.Length; i++)
            {
                scenesNames[i] = Path.GetFileNameWithoutExtension(buildScenes[i].path);
            }

            return scenesNames;
        }

        private int GetIndexOfScene(string[] scenesNames, string name)
        {
            for (var i = 0; i < scenesNames.Length; i++)
            {
                if (scenesNames[i].Equals(name)) return i;
            }

            return -1;
        }
    }
}