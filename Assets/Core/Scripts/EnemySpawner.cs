using UnityEngine;

namespace Core.Scripts
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameState _enableOnState;
        [SerializeField] private int _amountOfSpiders;
        [SerializeField] private GameObject _spiderPrefab;
        [SerializeField] private int _amountOfMonsters;
        [SerializeField] private GameObject _monsterPrefab;
        [SerializeField] private Transform _spawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (GameStateManager.Instance.State != _enableOnState) return;
            if (!other.gameObject.CompareTag("Player")) return;
            
            if (_amountOfMonsters > 0)
            {
                SpawnEnemy(_monsterPrefab, _amountOfMonsters);
            }

            if (_amountOfSpiders > 0)
            {
                SpawnEnemy(_spiderPrefab, _amountOfSpiders);
            }
            
            Destroy(gameObject);
        }

        private void SpawnEnemy(GameObject prefab, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(prefab, _spawnPoint.position, Quaternion.identity);
            }
        }
    }
}
