using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    public abstract class TransitionCondition : MonoBehaviour
    {
        protected bool _isSatisfied;
        public bool IsSatisfied { get; }
    }
}
