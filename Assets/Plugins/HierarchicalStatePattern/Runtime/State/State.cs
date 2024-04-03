using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace HierarchicalStatePattern
{
    [RequireComponent(typeof(TransitionManager))]
    public class State : AbstractState
    {
        private bool _entered = false;

        [Inject]
        private void Init()
        {
            if(!_entered) gameObject.SetActive(false);
        }

        public override void Enter()
        {
            _entered = true;
            gameObject.SetActive(true);
        }
        public override void Exit()
        {
            gameObject.SetActive(false);
        }
    }
}
