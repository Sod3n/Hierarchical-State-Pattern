using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    public class ToState : MonoBehaviour
    {
        [SerializeField] private AbstractState _state;

        public AbstractState State
        {
            get => _state;
            set => _state = value;
        }

        [Inject]
        protected void Initialize()
        {
            if(!gameObject.activeInHierarchy) return;
            
            var fromStates = transform.GetComponentsInChildren<FromState>();

            foreach (var from in fromStates)
            {
                if (!from)
                {
                    Debug.LogError("From is not assign to parent object", this);
                    return;
                }

                var manager = from.TransitionManager;

                from.TransitionData.State = _state;
            
                manager.Transitions.Value.Add(from.TransitionData);
            }
        }
    }
}