using System;
using UnityEngine;

namespace Core.Scripts.Achievements
{
    public class AchievementManager : MonoBehaviour
    {
        [SerializeField] private GameObject _achievementPrefab;
        
        public static AchievementManager Instance { get; private set; }

        private void Awake()
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
        
        // todo remove this method
        // now it is used just to test achievement getting
        private void Start()
        {
            foreach (Achievement achievement in Enum.GetValues(typeof(Achievement)))
            {
                PlayerPrefs.SetInt(((int)achievement).ToString(), 0);
            }
        }
        
        public void GenerateAchievement(Achievement achievement)
        {
            string achievementIdKey = ((int)achievement).ToString();
            if (PlayerPrefs.HasKey(achievementIdKey) && PlayerPrefs.GetInt(achievementIdKey) == 1) return;
            CollectableAchievement spawnedAchievement = Instantiate(_achievementPrefab, 
                Player.Instance.transform.position, Quaternion.identity).GetComponent<CollectableAchievement>();
            spawnedAchievement.InitializeAchievement(achievement);
        }
    }
}
