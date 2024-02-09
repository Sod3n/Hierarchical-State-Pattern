using Canopy.Events;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace HierarchicalStatePattern
{
    [CustomPropertyDrawer(typeof(TransitionList))]
    public class TransitionsPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(
            Rect position, 
            SerializedProperty property, 
            GUIContent label)
        {
            //Get desired property fields.
            EditorGUI.BeginProperty(position, GUIContent.none, property);

            var list = property.FindPropertyRelative("Value");
            //list.isExpanded = true;
            for (int i = 0; i < list.arraySize; i++)
            {
                var element = list.GetArrayElementAtIndex(i);

                var transition = element.GetSerializedValue<TransitionData>();
                var state = transition.State;
                var name = "State " + i;
                
                if (state) name = state.name;

                transition.Name = name;
            }

            var indent = EditorGUI.indentLevel;

            EditorGUI.indentLevel = -1;

            var listLabel = new GUIContent("Transitions");

            EditorGUILayout.PropertyField(list, listLabel);

            

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }

    public static class SerializedPropertyExtensions
    {
        public static T GetSerializedValue<T>(this SerializedProperty property)
        {
            object @object = property.serializedObject.targetObject;
            string[] propertyNames = property.propertyPath.Split('.');

            // Clear the property path from "Array" and "data[i]".
            if (propertyNames.Length >= 3 && propertyNames[propertyNames.Length - 2] == "Array")
                propertyNames = propertyNames.Take(propertyNames.Length - 2).ToArray();

            // Get the last object of the property path.
            foreach (string path in propertyNames)
            {
                @object = @object.GetType()
                    .GetField(path, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance)
                    .GetValue(@object);
            }

            if (@object.GetType().GetInterfaces().Contains(typeof(IList<T>)))
            {
                int propertyIndex = int.Parse(property.propertyPath[property.propertyPath.Length - 2].ToString());

                return ((IList<T>)@object)[propertyIndex];
            }
            else return (T)@object;
        }
    }


}
