using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    public class StateController : GameObjectContext
    {
        public AbstractState ParentState { get; set; }
        
        [SerializeField] private AbstractState _baseState;

        private AbstractState _nextState;
        private AbstractState _state;
        public AbstractState CurrentState
        {
            get
            {
                return _state;
            }
            set
            {
                if (value != null && value?.Controller != this) 
                {
                    ChangeState(value.Controller.ParentState);
                    value.Controller.ChangeState(value);
                }
                else
                {
                    _state?.Exit();
                    _state = value;
                    UniTask.WaitForFixedUpdate().ToUniTask().ContinueWith(() => _state?.Enter());
                    _nextState = _state;
                }
            }
        }
        protected override void RunInternal()
        {
            ParentState = transform.parent.GetComponent<AbstractState>();
            
            PreInstall += BindStateController;
            base.RunInternal();
        }

        private void BindStateController()
        {
            Container.BindInstance(this).AsSingle();
        }

        private void OnEnable()
        {
            if(_nextState == null)
                ChangeState(_baseState);
        }

        private void OnDisable()
        {
            ChangeState(null);
        }

        public void ChangeState(AbstractState state)
        {
            CurrentState = state;
        }
    }
}