using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HierarchicalStatePattern
{
    public abstract class StateBehaviour : MonoBehaviour
    {
        private void OnEnable()
        {
            OnEnter();
        }
        private void OnDisable()
        {
            OnExit();
        }
        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}
