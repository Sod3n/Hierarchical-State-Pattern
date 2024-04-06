using Cysharp.Threading.Tasks;
using UnityEngine;

namespace HierarchicalStatePattern
{
    public class FromState : Transition
    {
        [SerializeField] private TransitionManager _state;

        public TransitionManager TransitionManager
        {
            get => _state;
            set => _state = value;
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
            gameObject.SetActive(true);
            UniTask.NextFrame().ContinueWith(() => gameObject.SetActive(false));
        }
    }
}