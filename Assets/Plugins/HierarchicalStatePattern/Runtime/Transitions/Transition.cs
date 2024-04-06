using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    public class Transition : AbstractTransition
    {
        [SerializeField] protected TransitionData _transitionData;
        public override List<TransitionData> AddTransitionDataToList(List<TransitionData> list)
        {
            list.Add(_transitionData);
            return list;
        }

        public override int GetMaxTransitionCount()
        {
            return 1;
        }

        [Inject]
        protected virtual void Initialize()
        {
            _transitionData.EventReference.Initialize(gameObject);
        }
    }
}
