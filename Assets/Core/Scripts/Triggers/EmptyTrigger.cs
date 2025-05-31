using System.Linq;
using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    // used to allow triggers work on specified state
    public class EmptyTrigger : AvailableAtState
    {
        private Collider _collider;
        
        protected override void Start()
        {
            UseGravityAfterFirstInteraction = false;
            _collider = GetComponent<Collider>();
            base.Start();
        }

        protected void DisableCollider()
        {
            _collider.enabled = false;
        }
        
        protected override void TryEnable()
        {
            _collider.enabled = AvailableAtStates.Contains(StateManager.Instance.CurrentState);
        }
    }
}
