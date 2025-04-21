using System;
using UnityEngine;

namespace Core.Scripts
{
    public class GameStateManager : MonoBehaviour
    {
        [SerializeField] private GameState _defaultGameState;
        
        public event EventHandler<OnStateChangedEventArgs> OnStateChanged;
        public static GameStateManager Instance;
        public GameState State
        {
            get => _state;
            set
            {
                _state = value;
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs
                {
                    CurrentState = _state
                });
            }
        }
        public class OnStateChangedEventArgs : EventArgs
        {
            public GameState CurrentState;
        }

        private static GameState _state;

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
        }

        
        private void Start()
        {
            State = _defaultGameState;
        }
    }

    public enum GameState
    {
        Start,
        DarkNewTrain,
        OldManStory,
        OldTrain,
        ChildDefend,
        End
    }
}
