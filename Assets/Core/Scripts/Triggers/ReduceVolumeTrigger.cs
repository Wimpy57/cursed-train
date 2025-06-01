using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class ReduceVolumeTrigger : EmptyTrigger
    {
        [Header("Trigger settings")] 
        [SerializeField] private bool _returnVolumeOnTriggerExit;
        [SerializeField] private bool _disableWhenTriggerEnter;
        [Header("Audio clip to reduce")]
        [SerializeField] private AudioSource[] _audioSourcesToReduceVolume;
        [SerializeField] private float _reduceVolumeByPercentage;
        [SerializeField] private float _timeToReduceVolume;
        [SerializeField] private bool _reduceVolumeForever;

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            ReduceVolume();
            
            if (_disableWhenTriggerEnter)
            {
                DisableCollider();
            }
        }

        protected virtual void OnTriggerExit(Collider other)
        {
            if (!_returnVolumeOnTriggerExit) return;
            if (!other.gameObject.CompareTag("Player")) return;
            
            StopAllCoroutines();
            
            foreach (var item in _audioSourcesToReduceVolume)
            {
                StartCoroutine(SetVolumeTransition(item, item.volume / (1-_reduceVolumeByPercentage / 100f)));
            }

        }
        
        private void ReduceVolume()
        {
            foreach (var item in _audioSourcesToReduceVolume)
            {
                StartCoroutine(SetVolumeTransition(item,
                    item.volume - item.volume * (_reduceVolumeByPercentage / 100f)));
            }

            if (_reduceVolumeForever) return;

            StartCoroutine(ReduceVolumeByTime(_audioSourcesToReduceVolume, _timeToReduceVolume));
        }
        
        private IEnumerator ReduceVolumeByTime(AudioSource[] sources, float timeToReduce)
        {
            float timer = 0f;
            while (timer < timeToReduce)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            foreach (var item in sources)
            {
                StartCoroutine(SetVolumeTransition(item, item.volume / (1-_reduceVolumeByPercentage / 100f)));
            }
        }
        
        protected IEnumerator SetVolumeTransition(AudioSource source, float finalVolume, bool destroyAfterTransition = false)
        {
            float timer = 0f;
            float elapsedTime = 1f;
            float startVolume = source.volume;
            while (timer < elapsedTime)
            {
                source.volume = Mathf.Lerp(startVolume, finalVolume, timer / elapsedTime);
                timer += Time.deltaTime;
                yield return null;
            }

            if (destroyAfterTransition)
            {
                Destroy(source.gameObject);
            }
        }
    }
}