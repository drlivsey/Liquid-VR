using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Liquid.Translation
{
    [CustomEditor(typeof(TranslateSetuper))]
    public class TranslateSetuperEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var setuper = target as TranslateSetuper;
            
            if (GUILayout.Button("Find all translated objects"))
            {
                setuper.GatherTranslatedItems();
            }
        
            if (GUILayout.Button("Generate tamplates"))
            {
                setuper.GenerateTamplates();
            }
        }
    }
}