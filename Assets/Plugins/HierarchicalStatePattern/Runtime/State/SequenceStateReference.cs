using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    public class SequenceStateReference : AbstractState
    {
        [Inject] private StateController _stateController;
        [SerializeField] private List<AbstractState> _states;
        [SerializeField] private int _sequenceIndex = 0;
        [SerializeField] private float _resetTime = 0;

        private float _currentResetTime = 0;

        private int _SequenceIndex
        {
            get { return _sequenceIndex; }
            set
            {
                _sequenceIndex = value;
                if (_sequenceIndex >= _states.Count) _sequenceIndex = 0;
            }
        }


        public override void Enter()
        {
            if (_currentResetTime >= _resetTime) _SequenceIndex = 0;

            _currentResetTime = 0;
            _stateController.ChangeState(_states[_SequenceIndex]);
            _SequenceIndex++;
        }

        private void FixedUpdate()
        {
            if (_currentResetTime >= _resetTime) return;

            _currentResetTime += Time.fixedDeltaTime;
        }
    }
}
