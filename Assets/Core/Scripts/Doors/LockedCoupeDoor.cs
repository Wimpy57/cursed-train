using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Core.Scripts.Doors
{
    public class LockedCoupeDoor : Door, IStateChanger
    {
        [SerializeField] private SnapSocket _snapSocket;
        [SerializeField] private XRGrabInteractable _doorHandleOutside;
        [SerializeField] private GameObject _doorHandleOutsideVisual;

        protected override void Start()
        {
            base.Start();
            SnappableObject.OnObjectSnapped += SnappableObject_OnObjectSnapped;
        }

        private void SnappableObject_OnObjectSnapped(object sender, EventArgs e)
        {
            if (!_snapSocket.IsObjectSnapped) return;
            
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
            _doorHandleOutsideVisual.SetActive(true);
            _doorHandleOutside.enabled = true;
        }

        private void OnDisable()
        {
            SnappableObject.OnObjectSnapped -= SnappableObject_OnObjectSnapped;
        }
    }
}
