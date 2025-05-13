using System.Collections;
using Core.Scripts.Doors;
using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class ToiletEnterTrigger : MonoBehaviour, IStateChanger
    {
        [SerializeField] private Door _door;
     
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            StartCoroutine(LockDoor());
            StateManager.Instance.UpgradeState(this);
            
            // turn collider off not to change states more than once
            GetComponent<Collider>().enabled = false;
        }

        private IEnumerator LockDoor()
        {
            _door.Close();
            _door.Lock();
            yield return new WaitForSeconds(5f);
            _door.Unlock();
        }
    }
}
