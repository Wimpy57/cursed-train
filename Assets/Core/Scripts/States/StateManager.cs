using System;
using UnityEngine;

namespace Core.Scripts.States
{
    public class StateManager : MonoBehaviour
    {
        [SerializeField] private State _defaultState = 0;
        
        public static StateManager Instance { get; private set; }
        public event EventHandler OnStateChanged;
        
        public int TotalHpLost { get; private set; }
        public bool WasKeyDataStored { get; private set; }
        public bool WasSnapped { get; private set; }
        public int PlayerHpOnPreviousScene { get; private set; }
        public int TotalMonstersKilled { get; private set; }
        
        public Vector3 PreviousPlayerPosition { get; private set; }
        public Quaternion PreviousPlayerRotation { get; private set; }

        private bool _wasInitializedOnStart;
        
        
        public State CurrentState
        {
            get => _defaultState;
            private set
            {
                _defaultState = value;
                OnStateChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void Start()
        {
            Enemy.OnMonsterKilled += Enemy_OnMonsterKilled;
            Player.OnInstanceCreated += Player_OnInstanceCreated;
            SnapSocket.OnObjectUnsnapped += SnapSocket_OnObjectUnsnapped;
            SnappableObject.OnObjectSnapped += SnappableObject_OnObjectSnapped;
            _wasInitializedOnStart = true;
        }

        private void OnEnable()
        {
            if (!_wasInitializedOnStart)
            {
                Enemy.OnMonsterKilled += Enemy_OnMonsterKilled;
                Player.OnInstanceCreated += Player_OnInstanceCreated;
                SnapSocket.OnObjectUnsnapped += SnapSocket_OnObjectUnsnapped;
                SnappableObject.OnObjectSnapped += SnappableObject_OnObjectSnapped;
            }
        }

        private void Player_OnInstanceCreated(object sender, EventArgs e)
        {
            Player.Instance.OnHpChanged += Player_OnHpChanged;
            Player.Instance.OnKeyDataStored += Player_OnKeyDataStored;
        }

        private void SnapSocket_OnObjectUnsnapped(object sender, EventArgs e)
        {
            WasSnapped = false;
        }

        private void Enemy_OnMonsterKilled(object sender, EventArgs e)
        {
            TotalMonstersKilled++;
        }

        private void Player_OnKeyDataStored(object sender, EventArgs e)
        {
            WasKeyDataStored = true;
        }

        private void SnappableObject_OnObjectSnapped(object sender, EventArgs e)
        {
            WasSnapped = true;
        }

        private void Player_OnHpChanged(object sender, Player.OnHpChangedEventArgs e)
        {
            if (e.HpDifference < 0)
            {
                TotalHpLost += Math.Abs(e.HpDifference);
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

        public void SavePlayerInfo()
        {
            PlayerHpOnPreviousScene = Player.Instance.Hp;
            PreviousPlayerPosition = Player.Instance.transform.position;
            PreviousPlayerRotation = Player.Instance.transform.rotation;
        }

        public void UpgradeState(IStateChanger stateChanger)
        {
            if (stateChanger == null) return;
            
            CurrentState++;
        }

        public void Restart()
        {
            WasKeyDataStored = false;
            WasSnapped = false;
            PlayerHpOnPreviousScene = 100;
            TotalHpLost = 0;
            TotalMonstersKilled = 0;
            CurrentState = State.Menu;
        }

        public void OnDisable()
        {
            Enemy.OnMonsterKilled -= Enemy_OnMonsterKilled;               
            Player.OnInstanceCreated -= Player_OnInstanceCreated;
            Player.Instance.OnHpChanged -= Player_OnHpChanged;
            Player.Instance.OnKeyDataStored -= Player_OnKeyDataStored;
            SnapSocket.OnObjectUnsnapped -= SnapSocket_OnObjectUnsnapped;
            SnappableObject.OnObjectSnapped -= SnappableObject_OnObjectSnapped;
        }
    }
}
