using System.Linq;
using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class UpdateStateTrigger : AvailableAtState, IStateChanger
    {
        private Collider _collider;
    
        protected override void Start()
        {
            UseGravityAfterFirstInteraction = false;
            _collider = GetComponent<Collider>();
            base.Start();
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            StateManager.Instance.UpgradeState(this);
            
            // turn collider off not to change states more than once
            _collider.enabled = false;
        }
        
        protected override void TryEnable()
        {
            _collider.enabled = AvailableAtStates.Contains(StateManager.Instance.CurrentState);
        }
    }
}
