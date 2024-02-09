using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchicalStatePattern
{
    public abstract class AbstractTransition : MonoBehaviour
    {
        public abstract List<TransitionData> AddTransitionDataToList(List<TransitionData> list);
        public abstract int GetMaxTransitionCount();
    }
}
