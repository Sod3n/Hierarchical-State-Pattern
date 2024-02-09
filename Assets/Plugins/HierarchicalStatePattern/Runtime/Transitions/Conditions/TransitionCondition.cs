using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    public abstract class TransitionCondition : MonoBehaviour
    {
        public abstract bool IsSatisfied { get; }
    }
}
