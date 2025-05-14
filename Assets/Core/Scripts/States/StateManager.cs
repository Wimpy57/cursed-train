using System;
using UnityEngine;

namespace Core.Scripts.States
{
    public class StateManager : MonoBehaviour
    {
        [SerializeField] private State _defaultState = 0;
        
        public static StateManager Instance { get; private set; }
        public event EventHandler OnStateChanged;
        
        public State CurrentState
        {
            get => _defaultState;
            private set
            {
                _defaultState = value;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

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

        public void UpgradeState(IStateChanger stateChanger)
        {
            if (stateChanger == null) return;
            
            CurrentState++;
        }
    }
}
