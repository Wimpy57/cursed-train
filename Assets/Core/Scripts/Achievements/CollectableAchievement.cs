using UnityEngine;

namespace Core.Scripts.Achievements
{
    public class CollectableAchievement : MonoBehaviour
    {
        [SerializeField] private Achievement _achievement;

        public void InitializeAchievement(Achievement achievement)
        {
            _achievement = achievement;
        }
        
        public void GetAchievement()
        {
            // PlayerPrefs.SetInt(((int)_achievement).ToString(), 1);
            Destroy(gameObject);
        }
    }
}
