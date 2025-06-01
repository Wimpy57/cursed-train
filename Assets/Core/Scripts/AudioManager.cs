using System;
using System.Collections;
using System.Collections.Generic;
using Core.Scripts.Achievements;
using Core.Scripts.ScriptableObjects;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Core.Scripts
{
    public class AudioManager : MonoBehaviour
    {    
        [SerializeField] private float _volume = 1f;
        [SerializeField] private AudioRefsSO _audioRefsSO;
        [SerializeField] private GameObject _soundSourcePrefab;
        
        public static AudioManager Instance { get; private set; }
        
        private readonly Dictionary<AudioSource, float> _audioSourceDefaultVolume = new ();
        private AudioSource[] _audioSources;

        public void Awake()
        {
            if (Instance is null)
            {
                Instance = this;  
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }

        public void OnEnable()
        {
            _audioSources = Resources.FindObjectsOfTypeAll<AudioSource>();
            
            foreach (AudioSource audioSource in _audioSources)
            {
                _audioSourceDefaultVolume.Add(audioSource, audioSource.volume);
            }
            
            SubscribeOnSoundEvents();
        }

        private void SubscribeOnSoundEvents()
        {
            Player.OnWristKeyRead += Player_OnWristKeyRead;
            AchievementManager.OnAchievementSpawned += AchievementManager_OnAchievementSpawned;
        }

        private void UnsubscribeOnSoundEvents()
        {
            Player.OnWristKeyRead -= Player_OnWristKeyRead;
            AchievementManager.OnAchievementSpawned -= AchievementManager_OnAchievementSpawned;
        }

        private void AchievementManager_OnAchievementSpawned(object sender, EventArgs e)
        {
            PlayClip(_audioRefsSO.AchievementSpawnedSound, Player.Instance.transform.position);
        }
        
        private void Player_OnWristKeyRead(object sender, Player.OnWristKeyReadEventArgs e)
        {
            PlayClip(e.IsSuccess ? _audioRefsSO.CorrectSound : _audioRefsSO.IncorrectSound, e.Position);
        }

        public AudioSource PlayClip(AudioClip clip, Vector3 position, bool loop = false)
        {
            AudioSource audioSource = Instantiate(_soundSourcePrefab, position, Quaternion.identity)
                .GetComponent<AudioSource>();
            audioSource.clip = clip;
            audioSource.volume = GetVolume();
            audioSource.Play();
            if (!loop)
            {
                StartCoroutine(DestroySoundSource(audioSource, clip.length));
            }
            else
            {
                audioSource.loop = true;
            }
            
            return audioSource;
        }
        
        private void PlaySound(AudioClip[] clip, Vector3 position)
        {
            AudioSource.PlayClipAtPoint(clip[Random.Range(0, clip.Length)], position, GetVolume());
        }
        
        public void SetVolume(float volume)
        {
            _volume = volume > 1f ? 1f : volume < 0f ? 0f : volume;

            foreach (var audioSource in _audioSourceDefaultVolume)
            {
                audioSource.Key.volume = audioSource.Value * volume;
            }
        }

        public float GetVolume() => _volume;

        private IEnumerator DestroySoundSource(AudioSource soundSource, float delay)
        {
            yield return new WaitForSeconds(delay);
            Destroy(soundSource.gameObject);
        }
        
        private void OnDisable()
        {
            _audioSourceDefaultVolume.Clear();
            UnsubscribeOnSoundEvents();
        }
    }
}
