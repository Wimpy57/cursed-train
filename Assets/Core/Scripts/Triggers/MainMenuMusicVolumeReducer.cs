using System;
using System.Collections;
using System.Linq;
using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class MainMenuMusicVolumeReducer : AvailableAtState
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private float _timeToTurnOff;
        
        private Collider _collider;
        
        protected override void Start()
        {
            UseGravityAfterFirstInteraction = false;
            _collider = GetComponent<Collider>();
            base.Start();
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;

            StartCoroutine(ReduceVolume());
            
            // turn collider off not to change states more than once
            _collider.enabled = false;
        }

        private IEnumerator ReduceVolume()
        {
            float startVolume = _audioSource.volume;
           
            float elapsedTime = 0f;
            while (elapsedTime < _timeToTurnOff)
            {
                elapsedTime += Time.deltaTime;
                float currentValue = Mathf.Lerp(startVolume, 0f, elapsedTime / _timeToTurnOff);
                _audioSource.volume = currentValue;
                yield return null;
            }
            _audioSource.volume = 0f;
        }
        
        protected override void TryEnable()
        {
            _collider.enabled = AvailableAtStates.Contains(StateManager.Instance.CurrentState);
        }
    }
}
