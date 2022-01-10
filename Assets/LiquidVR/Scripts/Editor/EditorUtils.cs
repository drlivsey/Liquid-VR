using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Liquid.CustomInspector.Utils
{
    public static class EditorUtils
    {
        public static bool TryGetPropertiesByName(this SerializedObject target, string[] names, out SerializedProperty[] properties)
        {
            properties = new SerializedProperty[names.Length];
            for (var i = 0; i < names.Length; i++)
            {
                var property = target.FindProperty(names[i]);
                
                if (property == null) return false;
                
                properties[i] = property;
            }

            return true;
        }

        public static void DrawPropertiesFields(IEnumerable<SerializedProperty> properties, bool includeChildren = false)
        {
            foreach (var property in properties)
            {
                if (property == null) continue;

                EditorGUILayout.PropertyField(property, includeChildren);
            }
        }
    }
}
