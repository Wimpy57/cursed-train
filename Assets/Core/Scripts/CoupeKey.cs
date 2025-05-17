using Core.Scripts.States;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Core.Scripts
{
    public class CoupeKey : MonoBehaviour, IStateChanger
    {
        [SerializeField] private State _stateWhenCanUpgrade;
        
        private XRGrabInteractable _xrGrabInteractable;
        
        private void Start()
        {
            _xrGrabInteractable = GetComponent<XRGrabInteractable>();
            _xrGrabInteractable.selectEntered.AddListener(Interact);
        }

        private void Interact(SelectEnterEventArgs args)
        {
            if (StateManager.Instance.CurrentState != _stateWhenCanUpgrade) return;
            
            StateManager.Instance.UpgradeState(this);
            _xrGrabInteractable.selectEntered.RemoveListener(Interact);
        }
    }
}
