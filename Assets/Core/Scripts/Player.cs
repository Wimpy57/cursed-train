using System;
using System.Collections;
using Core.Scripts.States;
using Core.Scripts.UI;
using UnityEngine;

namespace Core.Scripts
{
    public class Player : MonoBehaviour
    {
        [Header("Player parameters")]
        [SerializeField] public int MaxHp;
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
        
        public bool IsKeyDataStored { get; private set; }
        
        
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
            if (Instance != null)
            {
                Hp = Instance.Hp;
                IsKeyDataStored = Instance.IsKeyDataStored;
                transform.position = _position;
                transform.rotation = _rotation;
            }
            else
            {
                Hp = MaxHp;
            }
            Instance = this;
        }
        
        private void Start()
        {
            // if (StateManager.Instance.CurrentState == State.DarkNewTrain && !_savePositionOnAnyState)
            // {
            //     transform.position = _toiletSpawnPosition.position;
            //     transform.rotation = _toiletSpawnPosition.rotation;
            // }
            // else if (StateManager.Instance.CurrentState != State.Menu && !_savePositionOnAnyState)
            // {
            //     transform.position = _conductorCoupeSpawnPosition.position;
            //     transform.rotation = _conductorCoupeSpawnPosition.rotation;
            // }
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

        public void StoreData()
        {
            IsKeyDataStored = true;
            _wristWatch.StoreDataVisual();
        }
        
        private void OnDisable()
        {
             _position = transform.position;
             _rotation = transform.rotation;
        }
    }
}
