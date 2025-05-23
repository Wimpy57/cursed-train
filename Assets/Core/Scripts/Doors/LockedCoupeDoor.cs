using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Core.Scripts.Doors
{
    public class LockedCoupeDoor : Door, IStateChanger
    {
        [SerializeField] private SnapSocket _snapSocket;
        [SerializeField] private XRGrabInteractable _doorHandleOutside;

        protected override void Start()
        {
            base.Start();
            SnapSocket.OnObjectUnsnapped += SnapSocket_OnObjectUnsnapped;
        }

        private void SnapSocket_OnObjectUnsnapped(object sender, EventArgs e)
        {
            if (!TryGetComponent(out SnapSocket snapSocket)) return;
            if (snapSocket != _snapSocket) return;
            
            _snapSocket.gameObject.SetActive(false);
            TryEnable();
        }

        protected override void TryEnable()
        {
            if (_snapSocket.IsObjectSnapped)
            {
                base.TryEnable();
            }
        }

        public override void Lock()
        {
            base.Lock();
            _doorHandleOutside.enabled = false;
        }

        public override void Unlock()
        {
            base.Unlock();
            _doorHandleOutside.enabled = true;
        }

        private void OnDisable()
        {
            SnapSocket.OnObjectUnsnapped -= SnapSocket_OnObjectUnsnapped;
        }
    }
}
