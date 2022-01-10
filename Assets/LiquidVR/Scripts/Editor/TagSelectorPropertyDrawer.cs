using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace Liquid.Core
{
    [CustomPropertyDrawer(typeof(TagSelectorAttribute), true)]
    public class TagSelectorPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.String)
            {
                EditorGUI.BeginProperty(position, label, property);
    
                var attrib = this.attribute as TagSelectorAttribute;
    
                if (attrib.UseDefaultTagFieldDrawer)
                {
                    property.stringValue = EditorGUI.TagField(position, label, property.stringValue);
                }
                else
                {
                    var tagList = new List<string>();
                    tagList.Add("<NoTag>");
                    tagList.AddRange(UnityEditorInternal.InternalEditorUtility.tags);
                    var propertyString = property.stringValue;
                    var index = -1;
                    if(propertyString == string.Empty)
                    {
                        index = 0; 
                    }
                    else
                    {
                        for (var i = 1; i < tagList.Count; i++)
                        {
                            if (tagList[i] == propertyString)
                            {
                                index = i;
                                break;
                            }
                        }
                    }
                    
                    index = EditorGUI.Popup(position, label.text, index, tagList.ToArray());
    
                    if(index == 0)
                    {
                        property.stringValue = string.Empty;
                    }
                    else if (index >= 1)
                    {
                        property.stringValue = tagList[index];
                    }
                    else
                    {
                        property.stringValue = string.Empty;
                    }
                }
    
                EditorGUI.EndProperty();
            }
            else
            {
                EditorGUI.PropertyField(position, property, label);
            }
        }
    }
 }