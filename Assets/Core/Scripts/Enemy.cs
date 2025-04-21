using UnityEngine;
using Core.Scripts.EnemyStateMachine;
using UnityEngine.AI;

namespace Core.Scripts
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected int Hp;
        
        [Header("Enemy states settings")]
        [SerializeField] public float DistanceToAggress;
        [SerializeField] public float DistanceToAttack;

        private EnemyState _currentState;
        private readonly IdleState _idleState = new();
        private readonly AggressedState _aggressedState = new();
        private readonly FightState _fightState = new();
        
        public NavMeshAgent Agent;
        
        private void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Extinguisher")) return;
            
            Hp--;
            if (Hp <= 0)
            {
                Destroy(gameObject);
            }
        }

        protected void Awake()
        {
            _currentState = _aggressedState;    
        }

        protected void Start()
        {
            Agent = GetComponent<NavMeshAgent>();
        }
        
        protected void Update()
        {
            UpdateState();
        }
        
        private void UpdateState()
        {
            _currentState.Behave(this);
        }
    }
}
