using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;

namespace Core.Scripts
{
    public class HandAnimator : MonoBehaviour
    {
        [SerializeField] private XRInputValueReader<float> _triggerInput = new ("Trigger");
        [SerializeField] private XRInputValueReader<float> _gripInput = new ("Grip");

        [SerializeField] private Animator _animator;
        
        private void Update()
        {
            _animator.SetFloat("Trigger", _triggerInput.ReadValue());
            _animator.SetFloat("Grip", _gripInput.ReadValue());
        }
    }
    
}
