using System;
using UnityEngine;

namespace Core.Scripts.Achievements
{
    public class AchievementManager : MonoBehaviour
    {
        [SerializeField] private GameObject _achievementPrefab;
        
        public static AchievementManager Instance { get; private set; }
        
        public static event EventHandler OnAchievementSpawned;

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
                
                // // todo remove this method
                // // now it is used just to test achievement getting
                // foreach (Achievement achievement in Enum.GetValues(typeof(Achievement)))
                // {
                //     // if (PlayerPrefs.HasKey(((int)achievement).ToString()))
                //     // {
                //     //     Debug.Log($"{achievement}: {PlayerPrefs.GetInt(((int)achievement).ToString())}");
                //     // }
                //     PlayerPrefs.SetInt(((int)achievement).ToString(), 0);
                // }
            }
            else
            {
                Destroy(gameObject);
            }
            DontDestroyOnLoad(gameObject);
        }
        
        public void GenerateAchievement(Achievement achievement)
        {
            if (IsAchieved(achievement)) return;
            CollectableAchievement spawnedAchievement = Instantiate(_achievementPrefab, 
                Player.Instance.transform.position + Vector3.up * 0.2f, 
                Quaternion.identity).GetComponent<CollectableAchievement>();
            spawnedAchievement.InitializeAchievement(achievement);
            OnAchievementSpawned?.Invoke(this, EventArgs.Empty);
        }

        public static bool IsAchieved(Achievement achievement)
        {
            string achievementIdKey = ((int)achievement).ToString();
            return PlayerPrefs.HasKey(achievementIdKey) && PlayerPrefs.GetInt(achievementIdKey) == 1;
        }
    }
}
