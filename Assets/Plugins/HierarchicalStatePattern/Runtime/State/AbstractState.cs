using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    public abstract class AbstractState : MonoBehaviour
    {
        public virtual void Enter() { }
        public virtual void Exit() { }

    }
}
