using System.Linq;
using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts.Doors
{
    public class Door : AvailableAtState
    {
        [SerializeField] protected GameObject[] Handles;
        [SerializeField] private DoorState _defaultState = 0;
        
        public bool IsLocked { get; private set; }
        
        private Vector3 _defaultPosition;
        private ConfigurableJoint _doorJoint;
        
        protected override void Start()
        {
            // todo fix this shi
            UseGravityAfterFirstInteraction = false;
            
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
            
            base.Start();
        }

        public void Close()
        {
            transform.position = _defaultPosition;
        }

        protected override void TryEnable()
        {
            if (AvailableAtStates.Contains(StateManager.Instance.CurrentState))
            {
                Unlock();
            }
        }

        public virtual void Lock()
        {
            _doorJoint.zMotion = ConfigurableJointMotion.Locked;
            foreach (GameObject handle in Handles)
            {
                handle.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
            }
            IsLocked = true;
        }

        public virtual void Unlock()
        {
            _doorJoint.zMotion = ConfigurableJointMotion.Limited;
            foreach (GameObject handle in Handles)
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
