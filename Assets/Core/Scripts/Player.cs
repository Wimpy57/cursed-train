using System;
using Core.Scripts.States;
using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] public int MaxHp;
        [SerializeField] private Transform _conductorCoupeSpawnPosition;
        [SerializeField] private Transform _toiletSpawnPosition;
        
        // used for debugging, will be deleted later
        [SerializeField] private bool _changePositionDependingOnState;
        
        public static Player Instance { get; private set; }
        
        public event EventHandler OnHpChanged;
        
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
            }
            Instance = this;
            // if (Instance == null)
            // {
            //     Instance = this;
            // }
            // else
            // {
            //     Destroy(gameObject);
            // }
            //DontDestroyOnLoad(gameObject);
        }
        
        private void Start()
        {
            Hp = MaxHp;
            if (StateManager.Instance.CurrentState == State.DarkNewTrain && _changePositionDependingOnState)
            {
                transform.position = _toiletSpawnPosition.position;
                transform.rotation = _toiletSpawnPosition.rotation;
            }
            else if (StateManager.Instance.CurrentState != State.Menu && _changePositionDependingOnState)
            {
                transform.position = _conductorCoupeSpawnPosition.position;
                transform.rotation = _conductorCoupeSpawnPosition.rotation;
            }
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
    }
}
