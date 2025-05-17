using System.Collections;
using System.Linq;
using Core.Scripts.Doors;
using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class LockDoorAndUpdateStateTrigger : AvailableAtState, IStateChanger
    {
        [SerializeField] private Door _door;
        [SerializeField] private float _timeToLockDoor;
        
        private Collider _collider;

        protected override void Start()
        {
            UseGravityAfterFirstInteraction = false;
            _collider = GetComponent<Collider>();
            base.Start();
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            StateManager.Instance.UpgradeState(this);
            StartCoroutine(LockDoor());
            
            // turn collider off not to change states more than once
            _collider.enabled = false;
        }

        protected override void TryEnable()
        {
            _collider.enabled = AvailableAtStates.Contains(StateManager.Instance.CurrentState);
        }

        private IEnumerator LockDoor()
        {
            _door.Close();
            _door.Lock();
            yield return new WaitForSeconds(_timeToLockDoor);
            _door.Unlock();
        }
    }
}
