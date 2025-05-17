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
                // if (PlayerPrefs.HasKey(((int)achievement).ToString()))
                // {
                //     Debug.Log($"{achievement}: {PlayerPrefs.GetInt(((int)achievement).ToString())}");
                // }
                PlayerPrefs.SetInt(((int)achievement).ToString(), 0);
            }
        }
        
        public void GenerateAchievement(Achievement achievement)
        {
            if (IsAchieved(achievement)) return;
            CollectableAchievement spawnedAchievement = Instantiate(_achievementPrefab, 
                Player.Instance.transform.position + Vector3.up * 0.2f, 
                Quaternion.identity).GetComponent<CollectableAchievement>();
            spawnedAchievement.InitializeAchievement(achievement);
        }

        public static bool IsAchieved(Achievement achievement)
        {
            string achievementIdKey = ((int)achievement).ToString();
            return PlayerPrefs.HasKey(achievementIdKey) && PlayerPrefs.GetInt(achievementIdKey) == 1;
        }
    }
}
