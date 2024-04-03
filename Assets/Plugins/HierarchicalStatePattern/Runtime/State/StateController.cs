using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    public class StateController : GameObjectContext
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

        protected override void RunInternal()
        {
            PreInstall += BindStateController;
            base.RunInternal();
        }

        private void BindStateController()
        {
            Container.Rebind<StateController>().AsSingle();
        }

        private void Start()
        {
            ChangeState(_baseState);
        }

        public void ChangeState(AbstractState state) { CurrentState = state; }
    }
}
