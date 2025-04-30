using System.Collections;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class ToiletEnterTrigger : MonoBehaviour
    {
        [SerializeField] private Door _door;
     
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            StartCoroutine(LockDoor());
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
