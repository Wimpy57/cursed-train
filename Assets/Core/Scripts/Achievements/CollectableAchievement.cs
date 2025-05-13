using System.Collections;
using UnityEngine;

namespace Core.Scripts.Achievements
{
    public class CollectableAchievement : MonoBehaviour
    {
        [SerializeField] private Achievement _achievement;

        private void Start()
        {
            if (AchievementManager.IsAchieved(_achievement))
            {
                Destroy(gameObject);
            }
        }
        
        public void InitializeAchievement(Achievement achievement)
        {
            _achievement = achievement;
            
            if (AchievementManager.IsAchieved(_achievement))
            {
                Destroy(gameObject);
            }
        }
        
        public void GetAchievement()
        {
            StartCoroutine(Disappear());
        }

        private IEnumerator Disappear()
        {
            // todo play animation and sound
            yield return null;
            PlayerPrefs.SetInt(((int)_achievement).ToString(), 1);
            Destroy(gameObject);
        }
    }
}
