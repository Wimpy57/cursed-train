using System.Collections;
using Core.Scripts.Doors;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class LockDoorAndUpdateStateTrigger : UpdateStateTrigger
    {
        [SerializeField] private Door _door;
        [SerializeField] private float _timeToLockDoor;
        
        protected override void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            base.OnTriggerEnter(other);
            StartCoroutine(LockDoor());
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
