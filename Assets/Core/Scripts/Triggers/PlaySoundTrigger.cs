using System.Collections;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class PlaySoundTrigger : EmptyTrigger
    {
        [Header("Trigger settings")] 
        [SerializeField] private bool _returnVolumeOnTriggerExit;
        [SerializeField] private bool _disableWhenTriggerEnter;
        [Header("New audio clip")]
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private Transform _spotToPlayClip;
        [Header("Audio clip to reduce")]
        [SerializeField] private AudioSource[] _audioSourcesToReduceVolume;
        [SerializeField] private float _reduceVolumeByPercentage;
        [SerializeField] private float _timeToReduceVolume;
        [SerializeField] private bool _reduceVolumeForever;

        private float _timer;
        private AudioSource _instantiatedAudio;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            _instantiatedAudio = AudioManager.Instance.PlayClip(_audioClip, _spotToPlayClip.position, true);
            ReduceVolume();
            if (_disableWhenTriggerEnter)
            {
                DisableCollider();
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!_returnVolumeOnTriggerExit) return;
            if (!other.gameObject.CompareTag("Player")) return;
            
            StopAllCoroutines();
            
            foreach (var item in _audioSourcesToReduceVolume)
            {
                StartCoroutine(SetVolumeTransition(item, item.volume / (1-_reduceVolumeByPercentage / 100f)));
            }

            StartCoroutine(SetVolumeTransition(_instantiatedAudio, 0f, true));
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

        private IEnumerator SetVolumeTransition(AudioSource source, float finalVolume, bool destroyAfterTransition = false)
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
