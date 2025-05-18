using System;
using System.Collections;
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

        private static Vector3 _position;
        private static Quaternion _rotation;
        
        
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
                transform.position = _position;
                transform.rotation = _rotation;
            }
            Instance = this;
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

        public IEnumerator Fade(float fadeDuration)
        {
            FadeEffect fadeEffect = GetComponentInChildren<FadeEffect>();
            yield return StartCoroutine(fadeEffect.Fade(true, fadeDuration));
        }

        // public IEnumerator UnFade(float fadeDuration)
        // {
        //     FadeEffect fadeEffect = GetComponentInChildren<FadeEffect>();
        //     yield return StartCoroutine(fadeEffect.UnFade(fadeDuration));
        // }
        
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

        private void OnDisable()
        {
             _position = transform.position;
             _rotation = transform.rotation;
        }
    }
}
