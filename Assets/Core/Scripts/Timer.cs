using System;
using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts
{
    public class Timer : MonoBehaviour
    {
        [SerializeField] private State _startsAtState;
        [SerializeField] private State _endsAtState;
        
        public static Timer Instance { get; private set; }

        private float _time;
        private bool _isGoing = true;

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
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
        }

        private void Update()
        {
            if (!_isGoing) return;
            _time += Time.deltaTime;
        }
        
        public float GetTime() => _time;
        
        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            if (StateManager.Instance.CurrentState == _startsAtState)
            {
                ResetTime();
                StartTimer();
            }

            if (StateManager.Instance.CurrentState == _endsAtState)
            {
                StopTimer();
            }
        }

        private void StopTimer()
        {
            _isGoing = false;
        }

        private void StartTimer()
        {
            _isGoing = true;
        }

        private void ResetTime()
        {
            _time = 0f;
            StopTimer();
        }
        

        private void OnDisable()
        {
            StateManager.Instance.OnStateChanged -= StateManager_OnStateChanged;
        }
    }
}
