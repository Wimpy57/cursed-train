using System.Collections;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class PlaySoundTrigger : ReduceVolumeTrigger
    {
        [Header("New audio clip")]
        [SerializeField] private AudioClip _audioClip;
        [SerializeField] private Transform _spotToPlayClip;
        [SerializeField] private bool _playLooped = true;

        private float _timer;
        private AudioSource _instantiatedAudio;
        
        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);
            if (!other.gameObject.CompareTag("Player")) return;
            _instantiatedAudio = AudioManager.Instance.PlayClip(_audioClip, _spotToPlayClip.position, _playLooped);
        }

        protected override void OnTriggerExit(Collider other)
        {
            base.OnTriggerExit(other);
            if (!other.gameObject.CompareTag("Player")) return;
            StartCoroutine(SetVolumeTransition(_instantiatedAudio, 0f, true));
        }
    }
}
