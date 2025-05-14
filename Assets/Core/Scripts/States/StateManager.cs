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

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Player.Instance.Hurt(10);
            }
        }

        public void UpgradeState(IStateChanger stateChanger)
        {
            if (stateChanger == null) return;
            
            CurrentState++;
        }
    }
}
