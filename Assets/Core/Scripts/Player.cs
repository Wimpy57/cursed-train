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
        [SerializeField] private SnapSocket _snapSocket;
        [Header("For debug only")]
        [SerializeField] private bool _savePositionOnAnyState;
        
        public static Player Instance { get; private set; }

        public static event EventHandler OnInstanceCreated;
        public event EventHandler<OnHpChangedEventArgs> OnHpChanged;
        public event EventHandler OnKeyDataStored;

        public class OnHpChangedEventArgs : EventArgs
        {
            public int HpDifference;
        }
        
        public int Hp
        {
            get => _hp;
            private set
            {
                if (value < 0 || value > MaxHp) return;
                
                int hpDifference = value - _hp;
                _hp = value;
                OnHpChanged?.Invoke(this, new OnHpChangedEventArgs
                {
                    HpDifference = hpDifference
                });
            }
        }

        private int _hp;
        
        private void Awake()
        {
            _isKeyDataStored = StateManager.Instance.WasKeyDataStored;
            Hp = StateManager.Instance.PlayerHpOnPreviousScene == 0 ? 
                MaxHp : StateManager.Instance.PlayerHpOnPreviousScene;
            
            Instance = this;
            OnInstanceCreated?.Invoke(this, EventArgs.Empty);
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
                if (isSuccess)
                {
                    OnKeyDataStored?.Invoke(this, EventArgs.Empty);
                }
            }
            _wristWatch.IndicateWatchOperation(isSuccess);
        }

        public void SnapObject(SnappableObject snappableObject)
        {
            _snapSocket.SnapObject(snappableObject);
        }
        
        public bool IsKeyDataStored() => _isKeyDataStored;
        
    }
}
