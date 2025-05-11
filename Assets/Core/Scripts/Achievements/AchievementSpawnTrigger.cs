using UnityEngine;

namespace Core.Scripts.Achievements
{
    public class AchievementSpawnTrigger : MonoBehaviour
    {
        [SerializeField] private Achievement _achievementToSpawn;
        [SerializeField] private GameObject _achievementCoinPrefab;
        [SerializeField] private Transform _pointToSpawnCoin;
        
        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            
            CollectableAchievement collectableAchievement = Instantiate(_achievementCoinPrefab, 
                _pointToSpawnCoin.position, Quaternion.identity)
                .GetComponent<CollectableAchievement>();
            collectableAchievement.InitializeAchievement(_achievementToSpawn);
            Destroy(gameObject);
        }
    }
}
