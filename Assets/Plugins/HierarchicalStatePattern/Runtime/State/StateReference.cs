using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    public class StateReference : AbstractState
    {
        [Inject] private StateController _stateController;
        [SerializeField] private AbstractState _state;

        public override void Enter()
        {
            _stateController.ChangeState(_state);
        }
    }
}
