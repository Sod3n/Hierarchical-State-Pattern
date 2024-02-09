using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchicalStatePattern
{
    public class TransitionGroup : AbstractTransition
    {
        [SerializeField] private List<AbstractTransition> _transitions;

        public override List<TransitionData> AddTransitionDataToList(List<TransitionData> list)
        {
            _transitions.ForEach(x => x.AddTransitionDataToList(list));
            return list;
        }

        public override int GetMaxTransitionCount()
        {
            int count = 0;
            _transitions.ForEach(_x => count += _x.GetMaxTransitionCount()); 
            return count; 
        }
    }
}
