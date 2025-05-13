using System.Collections;
using UnityEngine;

namespace Core.Scripts.Achievements
{
    public class CollectableAchievement : MonoBehaviour
    {
        [SerializeField] private Achievement _achievement;

        public void InitializeAchievement(Achievement achievement)
        {
            if (AchievementManager.IsAchieved(achievement))
            {
                Destroy(gameObject);
            }
            _achievement = achievement;
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
