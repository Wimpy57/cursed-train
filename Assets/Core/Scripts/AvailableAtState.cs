using System;
using System.Linq;
using Core.Scripts.States;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Core.Scripts
{
    public class AvailableAtState : MonoBehaviour
    {
        [SerializeField] private State[] _availableAtStates;

        private XRGrabInteractable _xrGrabInteractable;

        private void Start()
        {
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();
            TryEnable();
            
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
        }

        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            TryEnable();
        }

        private void TryEnable()
        {
            _xrGrabInteractable.enabled = _availableAtStates.Contains(StateManager.Instance.CurrentState);
        }

        private void OnDisable()
        {
            StateManager.Instance.OnStateChanged -= StateManager_OnStateChanged;
        }
    }
}
