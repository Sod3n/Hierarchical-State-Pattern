using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;
using System;
using System.Reflection;
using UnityEngine.UIElements;


#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Canopy.Events
{
    /// <summary>
    /// Eventlistener class to add a editor reference to an UnityEvent. This would allow for the other way event listening.
    /// </summary>
    [System.Serializable]
    public class EventRef
    {
        //Asset the event is contained within.
        [SerializeField] private MonoBehaviour _eventAsset;

        [SerializeField] private string _eventField;

        private EventInfo _eventInfo = null;
        private GameObject _gameObject;

        public static EventRef operator +(EventRef a, Action b)
        {
            if (a._eventInfo == null)
            {
                Debug.LogError("Event Info is not found", a._gameObject);
                return a;
            }

            a._eventInfo.AddEventHandler(a._eventAsset, b);
            return a;
        }
        public static EventRef operator -(EventRef a, Action b)
        {
            if (a._eventInfo == null)
            {
                Debug.LogError("Event Info is not found", a._gameObject);
                return a;
            }

            a._eventInfo.RemoveEventHandler(a._eventAsset, b);
            return a;
        }


        public void Initialize(GameObject gameObject = null)
        {
            _gameObject = gameObject;
            if (_eventAsset == null)
            {
                Debug.LogError("Event Reference is not set", _gameObject);
                return;
            }

            _eventInfo = _eventAsset.GetType().GetEvent(_eventField);
        }

    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(EventRef))]
    public class EventListenerPropertyDrawer : PropertyDrawer
    {
        SerializedProperty property = null;

        public override void OnGUI(
            Rect position, 
            SerializedProperty property, 
            GUIContent label)
        {
            // Get desired property fields.
            EditorGUI.BeginProperty(position, GUIContent.none, property);
            SerializedProperty eventAsset = property.FindPropertyRelative("_eventAsset");
            SerializedProperty eventFieldProperty = property.FindPropertyRelative("_eventField");


            EditorGUI.BeginDisabledGroup(Application.isPlaying);

            // Show Label
            position = EditorGUI.PrefixLabel(
                            position, 
                            GUIUtility.GetControlID(FocusType.Passive), 
                            label);

            // Set Indent Level
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            position.width /= 2;


            // Show Object Field
            var objectPosition = position;
            objectPosition.x += objectPosition.width;

            // check if object is on scene or is asset
            bool onScene = !EditorUtility.IsPersistent(property.exposedReferenceValue);


            eventAsset.objectReferenceValue = EditorGUI.ObjectField(
                objectPosition,
                GUIContent.none,
                eventAsset.objectReferenceValue,
                typeof(MonoBehaviour),
                onScene);


            // Check needs of display dropdown
            if (eventAsset == null ||
                this.property == property ||
                eventAsset.objectReferenceValue == null)
            {

                EditorGUI.EndProperty();
                return;
            }
            
            this.property = property;
            var events = eventAsset.objectReferenceValue
                .GetType()
                .GetEvents()
                .ToList();

            var selectedEventIndex = events.FindIndex
                (0, e => e.Name == eventFieldProperty.stringValue);

            var eventsNameList = events.Select(e => e.Name).ToArray();

            // Display dropdown and set index and field name.
            Rect popupRect = new Rect(position);
            popupRect.xMax = objectPosition.xMin;

            selectedEventIndex = EditorGUI.Popup(popupRect, selectedEventIndex, eventsNameList);

            eventFieldProperty.stringValue = events.ElementAtOrDefault(selectedEventIndex)?.Name;


            
            EditorGUI.indentLevel = indent;

            EditorGUI.EndDisabledGroup();

            EditorGUI.EndProperty();
        }

    }
#endif

}
