using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Core.Scripts
{
    public class SetChildOnGrab : MonoBehaviour
    {
        private void Start()
        {
            XRGrabInteractable grabInteractable = GetComponent<XRGrabInteractable>();
            grabInteractable.selectEntered.AddListener(OnSelectEnter);
            grabInteractable.selectExited.AddListener(OnSelectExit);
        }

        private void OnSelectEnter(SelectEnterEventArgs e)
        {
            gameObject.transform.SetParent(e.interactorObject.transform);
        }

        private void OnSelectExit(SelectExitEventArgs e)
        {
            gameObject.transform.SetParent(null);
        }
    }
}
