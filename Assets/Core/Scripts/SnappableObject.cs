using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Core.Scripts
{
    public class SnappableObject : MonoBehaviour
    {
        [SerializeField] private bool _setToInventoryOnSelectEnter;
        
        
        [SerializeField] private XRGrabInteractable _xrGrabInteractable;
        [SerializeField] private Rigidbody _rigidbody;
        
        public static event EventHandler OnObjectSnapped;
        
        private SnapSocket _snapSocket;
        
        private void Start()
        {
            if (_setToInventoryOnSelectEnter)
            {
                _xrGrabInteractable.selectEntered.AddListener(TakeToInventory);
            }
        }

        private void TakeToInventory(SelectEnterEventArgs args)
        {
            SetGrabEnabled(false);
            Player.Instance.SnapObject(this);
            if (_setToInventoryOnSelectEnter)
            {
                _xrGrabInteractable.selectEntered.RemoveListener(TakeToInventory);
            }
        }

        private void SetGrabEnabled(bool isEnabled)
        { 
            //_rigidbody.isKinematic = isEnabled;
            _xrGrabInteractable.enabled = isEnabled;
        }

        public void SetSnapSocket(SnapSocket snapSocket)
        {
            if (snapSocket is null)
            {
                _snapSocket = null;
                SetGrabEnabled(true);
                return;
            }
            
            OnObjectSnapped?.Invoke(this, EventArgs.Empty);
            _snapSocket = snapSocket;
            SetGrabEnabled(true);
                
            _rigidbody.useGravity = false;
            gameObject.transform.SetParent(snapSocket.transform);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            gameObject.transform.localRotation = Quaternion.identity;
            
            _xrGrabInteractable.selectExited.AddListener(OnSelectExitWhileSnapped);
        }

        public bool TryGetSnapSocket(out SnapSocket snapSocket)
        {
            snapSocket = null;
            
            if (_snapSocket is null) return false;
            snapSocket = _snapSocket;
            return true;
        }

        private void OnSelectExitWhileSnapped(SelectExitEventArgs arg0)
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            gameObject.transform.localRotation = Quaternion.identity;
        }
    }
}
