using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class NpcEnemySpawner : MonoBehaviour
    {
        [SerializeField] private State _enableOnState;
        [SerializeField] private int _amountOfSpiders;
        [SerializeField] private GameObject _spiderPrefab;
        [SerializeField] private int _amountOfMonsters;
        [SerializeField] private GameObject _monsterPrefab;
        [SerializeField] private int _amountOfOldMen;
        [SerializeField] private GameObject _oldManPrefab;
        [SerializeField] private Transform _spawnPoint;

        private void OnTriggerEnter(Collider other)
        {
            if (StateManager.Instance.CurrentState != _enableOnState) return;
            if (!other.gameObject.CompareTag("Player")) return;
            
            if (_amountOfMonsters > 0)
            {
                Spawn(_monsterPrefab, _amountOfMonsters);
            }

            if (_amountOfSpiders > 0)
            {
                Spawn(_spiderPrefab, _amountOfSpiders);
            }

            if (_amountOfOldMen > 0)
            {
                Spawn(_oldManPrefab, 1);
            }
            
            Destroy(gameObject);
        }

        private void Spawn(GameObject prefab, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                Instantiate(prefab, _spawnPoint.position, Quaternion.identity);
            }
        }
    }
}
