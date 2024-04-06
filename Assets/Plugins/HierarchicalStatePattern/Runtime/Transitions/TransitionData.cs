using Canopy.Events;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchicalStatePattern
{
    [Serializable]
    public class TransitionData
    {
        //Name of element of list in inspector
        [HideInInspector] public string Name = "State";

        [HideInInspector] public AbstractState State;
        public EventRef EventReference;
        public List<TransitionCondition> Conditions = new List<TransitionCondition>();
        public List<TransitionCondition> InversedConditions = new List<TransitionCondition>();
    }
}
