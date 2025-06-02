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
        private bool _isGoing;

        private float _previousTime = 0f;
        private void Awake()
        {
            if (Instance is null)
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
            CheckState();
        }

        private void Update()
        {
            if (!_isGoing) return;
            _time += Time.deltaTime;

            if (_time >= _previousTime + 1f)
            {
                _previousTime = _time;
            }
        }
        
        public float GetTime() => _time;
        
        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            CheckState();
        }

        private void CheckState()
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
