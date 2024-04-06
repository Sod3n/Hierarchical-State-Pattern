using UnityEngine;

namespace HierarchicalStatePattern
{
    public class FromState : MonoBehaviour
    {
        [SerializeField] private TransitionManager _transitionManager;

        public TransitionManager TransitionManager
        {
            get => _transitionManager;
            set => _transitionManager = value;
        }
        
    }
}