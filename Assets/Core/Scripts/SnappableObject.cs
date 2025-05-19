using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Core.Scripts
{
    public class SnappableObject : MonoBehaviour
    {
        [SerializeField] private bool _setToInventoryOnSelectEnter;
        
        public static event EventHandler OnObjectSnapped;
        
        private XRGrabInteractable _xrGrabInteractable;
        private SnapSocket _snapSocket;
        
        private void Start()
        {
            Debug.Log("start");
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();
            if (_setToInventoryOnSelectEnter)
            {
                _xrGrabInteractable.selectEntered.AddListener(OnSelectEnter);
            }
        }

        private void OnSelectEnter(SelectEnterEventArgs args)
        {
            SetGrabEnabled(false);
            Player.Instance.SnapObject(this);
            if (_setToInventoryOnSelectEnter)
            {
                _xrGrabInteractable.selectEntered.RemoveListener(OnSelectEnter);
            }
        }

        private void SetGrabEnabled(bool isEnabled)
        {
            Debug.Log("set enabled");
            GetComponent<Rigidbody>().isKinematic = isEnabled;
            _xrGrabInteractable.enabled = isEnabled;
        }

        public void SetSnapSocket(SnapSocket snapSocket)
        {
            if (snapSocket == null)
            {
                _snapSocket = null;
                SetGrabEnabled(true);
                return;
            }
            
            OnObjectSnapped?.Invoke(this, EventArgs.Empty);
            _snapSocket = snapSocket;
            SetGrabEnabled(true);
            
            gameObject.GetComponent<Rigidbody>().useGravity = false;
            gameObject.transform.SetParent(snapSocket.transform);
            gameObject.transform.localScale = new Vector3(1, 1, 1);
            gameObject.transform.localPosition = new Vector3(0, 0, 0);
            gameObject.transform.localRotation = Quaternion.identity;
            
            _xrGrabInteractable.selectExited.AddListener(OnSelectExitWhileSnapped);
        }

        public bool TryGetSnapSocket(out SnapSocket snapSocket)
        {
            snapSocket = null;
            
            if (_snapSocket == null) return false;
            snapSocket = _snapSocket;
            return true;
        }

        private void OnSelectExitWhileSnapped(SelectExitEventArgs arg0)
        {
            Player.Instance.SnapObject(this);
        }
    }
}
