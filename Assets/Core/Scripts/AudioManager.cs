using UnityEngine;

namespace Core.Scripts
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField] private float _volume = 1f;
        [SerializeField] public AudioSource[] AudioSources;
        
        public static AudioManager Instance { get; private set; }

        public void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void SetVolume(float volume)
        {
            _volume = volume > 1f ? 1f : volume < 0f ? 0f : volume;

            foreach (var audioSource in AudioSources)
            {
                audioSource.volume = _volume;
            }
        }
    }
}
