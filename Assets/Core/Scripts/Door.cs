using UnityEngine;

namespace Core.Scripts
{
    public class Door : MonoBehaviour
    {
        public bool IsLocked { get; private set; }
        
        private Vector3 _defaultPosition;
        private ConfigurableJoint _doorJoint;
        
        private void Start()
        {
            _doorJoint = GetComponent<ConfigurableJoint>();
            _defaultPosition = transform.position;
        }

        public void Close()
        {
            transform.position = _defaultPosition;
        }

        public void Lock()
        {
            _doorJoint.zMotion = ConfigurableJointMotion.Locked;
            IsLocked = true;
        }

        public void Unlock()
        {
            _doorJoint.zMotion = ConfigurableJointMotion.Limited;
            IsLocked = false;
        }
    }
}
