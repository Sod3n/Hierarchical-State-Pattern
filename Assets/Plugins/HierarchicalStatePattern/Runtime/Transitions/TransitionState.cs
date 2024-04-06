using System;
using Cysharp.Threading.Tasks;

namespace HierarchicalStatePattern
{
    public class TransitionState : Transition
    {
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