using Canopy.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    public class TransitionManager : StateBehaviour, IInitializable
    {
        [Inject] private StateController _stateController;
        [SerializeField] private List<AbstractTransition> _references = new List<AbstractTransition>();
        [SerializeField] private TransitionList _transitions = new TransitionList();


        private TransitionList _transitionList = new TransitionList();

        private Dictionary<EventRef, Action> _transitionActions = new Dictionary<EventRef, Action>();

        [Inject]
        public void Initialize()
        {
            int count = _references.Count;
            _references.ForEach(x => count += x.GetMaxTransitionCount());
            _transitionActions.EnsureCapacity(count);

            _transitions.Value.ForEach(x => x.EventReference.Initialize());
        }

        public override void OnEnter()
        {
            RefillTransitionData();
            SubscribeTransitions();
        }
        public override void OnExit()
        {
            UnsubscribeTransitions();
        }

        private void RefillTransitionData()
        {
            _transitionList.Value.Clear();
            _references.ForEach(x => x.AddTransitionDataToList(_transitionList.Value));
            _transitions.Value.ForEach(x => _transitionList.Value.Add(x));
        }

        private void SubscribeTransitions()
        {
            foreach (var data in _transitionList.Value)
            {
                Action action = () => 
                { 
                    if(data.Conditions.TrueForAll(x => x.IsSatisfied)) 
                        _stateController.ChangeState(data.State);  
                };

                data.EventReference += action;
                _transitionActions.Add(data.EventReference, action);
            }
        }
        private void UnsubscribeTransitions()
        {
            foreach (var data in _transitionList.Value)
            {
                data.EventReference -= _transitionActions[data.EventReference];
            }
            _transitionActions.Clear();
        }
    }
}
