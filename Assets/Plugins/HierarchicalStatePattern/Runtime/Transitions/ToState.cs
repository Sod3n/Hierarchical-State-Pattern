using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HierarchicalStatePattern
{
    [RequireComponent(typeof(Transition))]
    public class ToState : Transition
    {
        [SerializeField] private AbstractState _state;

        protected override void Initialize()
        {
            base.Initialize();
            
            var from = transform.parent.GetComponent<FromState>();

            if (!from)
            {
                Debug.LogError("From is not assign to parent object", this);
                return;
            }

            var manager = from.TransitionManager;

            _transitionData.State = _state;
            
            manager.Transitions.Value.Add(_transitionData);
            
            _transitionData.EventReference += ReDeactivateState;
            gameObject.SetActive(false);
        }
        
        
        private void ReDeactivateState()
        {
            gameObject.SetActive(true);
            UniTask.NextFrame().ContinueWith(() => gameObject.SetActive(false));
        }
    }
}