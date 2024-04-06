﻿using UnityEditor;

namespace HierarchicalStatePattern
{
    [CustomEditor(typeof(FromState))]
    public class FromStateEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var fromState = (FromState) target;
            fromState.gameObject.name = "From"+(fromState.TransitionManager != null ? fromState.TransitionManager.gameObject.name : "State");
        }
    }
}