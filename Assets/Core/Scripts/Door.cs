using UnityEngine;

namespace Core.Scripts
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private GameObject[] _handles;
        [SerializeField] private DoorState _defaultState = 0;
        
        public bool IsLocked { get; private set; }
        
        private Vector3 _defaultPosition;
        private ConfigurableJoint _doorJoint;
        
        private void Start()
        {
            _doorJoint = GetComponent<ConfigurableJoint>();
            _defaultPosition = transform.position;
            switch (_defaultState)
            {
                case DoorState.Locked:
                    Close();
                    Lock();
                    break;
                case DoorState.Unlocked:
                    Unlock();
                    break;
            }
        }

        public void Close()
        {
            transform.position = _defaultPosition;
        }

        public void Lock()
        {
            _doorJoint.zMotion = ConfigurableJointMotion.Locked;
            foreach (GameObject handle in _handles)
            {
                handle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            }
            IsLocked = true;
        }

        public void Unlock()
        {
            _doorJoint.zMotion = ConfigurableJointMotion.Limited;
            foreach (GameObject handle in _handles)
            {
                handle.GetComponent<Rigidbody>().constraints = (RigidbodyConstraints)6;
            }
            IsLocked = false;
        }
    }

    public enum DoorState
    {
        Unlocked,
        Locked,
    }
}
