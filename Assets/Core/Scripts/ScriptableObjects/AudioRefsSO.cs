using UnityEngine;

namespace Core.Scripts.ScriptableObjects
{
    [CreateAssetMenu]
    public class AudioRefsSO : ScriptableObject
    {
        public AudioClip IncorrectSound;
        public AudioClip CorrectSound;
        public AudioClip AchievementSpawnedSound;
    }
}
