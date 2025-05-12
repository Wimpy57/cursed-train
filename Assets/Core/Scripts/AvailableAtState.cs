using System;
using System.Linq;
using Core.Scripts.States;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Core.Scripts
{
    public class AvailableAtState : MonoBehaviour
    {
        [SerializeField] protected State[] AvailableAtStates;
        [SerializeField] private bool _useGravityAfterFirstInteraction;

        private XRGrabInteractable _xrGrabInteractable;
        private Rigidbody _rigidbody;

        protected virtual void Start()
        {
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();
            TryEnable();
            
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
            if (_useGravityAfterFirstInteraction)
            {
                _rigidbody = gameObject.GetComponent<Rigidbody>();
                _rigidbody.useGravity = false;
                _xrGrabInteractable.firstSelectEntered.AddListener(OnFirstSelection);
            }
        }

        public void OnFirstSelection(SelectEnterEventArgs args)
        {
            _rigidbody.useGravity = true;
        }
        
        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            TryEnable();
        }

        protected virtual void TryEnable()
        {
            _xrGrabInteractable.enabled = AvailableAtStates.Contains(StateManager.Instance.CurrentState);
        }

        private void OnDisable()
        {
            StateManager.Instance.OnStateChanged -= StateManager_OnStateChanged;
        }
    }
}
