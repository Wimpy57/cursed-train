using Core.Scripts.Doors;
using UnityEngine;

namespace Core.Scripts
{
    public class DoorOpenerSnapSocket : SnapSocket
    {
        [Header("Door parameters")] 
        [SerializeField] private GameObject _handleToEnable;
        [SerializeField] private Door _doorToUnlock;
        
        public override void SnapObject(SnappableObject snappableObject)
        {
            base.SnapObject(snappableObject);
            _handleToEnable.SetActive(true);
            _doorToUnlock.Unlock();
            Destroy(snappableObject);
        }
    }
}
