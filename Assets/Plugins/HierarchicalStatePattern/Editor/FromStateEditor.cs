using UnityEditor;

namespace HierarchicalStatePattern
{
    [CustomEditor(typeof(ToState))]
    public class ToStateEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var toState = (ToState) target;
            toState.gameObject.name = "To"+(toState.State != null ? toState.State.name : "State");
        }
    }
}