using System;
using System.Collections;
using Core.Scripts.Scenes;
using Core.Scripts.States;
using Core.Scripts.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scripts
{
    public class Player : MonoBehaviour
    {
        [Header("Player parameters")]
        [SerializeField] public int MaxHp;
        [SerializeField] private bool _isKeyDataStored;
        [Header("Spawn points")]
        [SerializeField] private Transform _conductorCoupeSpawnPosition;
        [SerializeField] private Transform _toiletSpawnPosition;
        [Header("Linked objects")]
        [SerializeField] private WristMenu _wristWatch;
        [SerializeField] private FadeEffect _fadeEffect;
        [Header("For debug only")]
        [SerializeField] private bool _savePositionOnAnyState;
        
        public static Player Instance { get; private set; }
        
        //todo reset total hp lost
        public static int TotalHpLost { get; private set; }
        
        public event EventHandler OnHpChanged;

        private static Vector3 _position;
        private static Quaternion _rotation;
        private static bool _wasKeyDataStored;
        private static int _previousSceneHp;
        
        public int Hp
        {
            get => _hp;
            private set
            {
                _hp = value;
                OnHpChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private int _hp;
        
        private void Awake()
        {
            _isKeyDataStored = _wasKeyDataStored;
            // transform.position = _position;
            // transform.rotation = _rotation;
            Hp = _previousSceneHp == 0 ? MaxHp : _previousSceneHp;
            
            Instance = this;
        }
        
        private void Start()
        {
            SceneName currentScene = SceneName.NewTrainScene;
            foreach (var item in SceneInfo.SceneStringNameDictionary)
            {
                if (item.Value == SceneManager.GetActiveScene().name) currentScene = item.Key;
            }

            if (currentScene != SceneName.DarkNewTrainScene) return;
            
            if (StateManager.Instance.CurrentState == State.DarkNewTrain && !_savePositionOnAnyState)
            {
                transform.position = _toiletSpawnPosition.position;
                transform.rotation = _toiletSpawnPosition.rotation;
            }
            else if (StateManager.Instance.CurrentState != State.Menu && !_savePositionOnAnyState)
            {
                transform.position = _conductorCoupeSpawnPosition.position;
                transform.rotation = _conductorCoupeSpawnPosition.rotation;
            }
        }

        public IEnumerator Fade(float fadeDuration)
        {
            yield return StartCoroutine(_fadeEffect.Fade(true, fadeDuration));
        }
        
        public void Hurt(int damage)
        {
            Hp -= damage;
            TotalHpLost += damage;
            if (Hp <= 0)
            {
                //todo player is dead
            }
        }
        
        public void Heal(int heal)
        {
            Hp += heal;
        }

        public void OperateWithWatchData(bool isSuccess, bool isKeyDataChanged=false)
        {
            if (isKeyDataChanged)
            {
                _isKeyDataStored = isSuccess;
            }
            _wristWatch.IndicateWatchOperation(isSuccess);
        }

        public bool IsKeyDataStored() => _isKeyDataStored;
        
        private void OnDisable()
        {
             // _position = transform.position;
             // _rotation = transform.rotation;
             _wasKeyDataStored = _isKeyDataStored;
        }
    }
}
