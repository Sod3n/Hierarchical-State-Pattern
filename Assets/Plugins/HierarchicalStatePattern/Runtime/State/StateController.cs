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
                    value.Controller.ChangeState(value);
                    ChangeState(value.Controller.ParentState);
                }
                else
                {
                    _state?.Exit();
                    _state = value;
                    UniTask.NextFrame().ContinueWith(() => _state?.Enter());
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