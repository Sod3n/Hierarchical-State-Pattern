using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    public class StateController : MonoBehaviour
    {
        [SerializeField] private AbstractState _baseState;
        private AbstractState _state;
        public AbstractState CurrentState
        {
            get
            {
                return _state;
            }
            set
            {
                _state?.Exit();
                _state = value;
                _state?.Enter();
            }
        }


        private void Start() { ChangeState(_baseState); }

        public void ChangeState(AbstractState state) { CurrentState = state; }
    }
}
