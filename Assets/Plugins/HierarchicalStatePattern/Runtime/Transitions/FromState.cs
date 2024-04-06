using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    public class FromState : Transition
    {
        [Inject] private StateController _stateController;
        
        [SerializeField] private State _state;

        public State State
        {
            get => _state;
            set => _state = value;
        }

        private TransitionManager _transitionManager;
        
        public TransitionManager TransitionManager
        {
            get => _transitionManager ? _transitionManager : GetTransitionManager();
            set => _transitionManager = value;
        }

        public TransitionData TransitionData
        {
            get => _transitionData;
            set => _transitionData = value;
        }

        protected override void Initialize()
        {
            base.Initialize();
            
            _transitionData.EventReference += ReDeactivateState;
            gameObject.SetActive(false);
        }
        
        
        private void ReDeactivateState()
        {
            if(_stateController.CurrentState != _state) return;
            
            gameObject.SetActive(true);
            UniTask.NextFrame().ContinueWith(() => gameObject.SetActive(false));
        }

        private TransitionManager GetTransitionManager()
        {
            _transitionManager = _state.GetComponent<TransitionManager>();
            return _transitionManager;
        }
    }
}