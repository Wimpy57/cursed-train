using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class UpdateStateTrigger : EmptyTrigger, IStateChanger
    {
        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            StateManager.Instance.UpgradeState(this);
            
            DisableCollider();
        }
    }
}
