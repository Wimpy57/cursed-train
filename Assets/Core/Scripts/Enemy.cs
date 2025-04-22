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
        [SerializeField] public float AggressedStateSpeed;
        
        private NavMeshAgent _agent;
        
        private EnemyState _currentState;
        
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
            _currentState = new IdleState();    
        }

        protected void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
        }
        
        protected void Update()
        {
            UpdateState();
        }
        
        private void UpdateState()
        {
            _currentState.Behave(this);
        }

        public void ChangeState(EnemyState state)
        {
            _currentState = state;
            _currentState.Enter(this);
        }

        public void SetDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }

        public void SetSpeed(float speed)
        {
            _agent.speed = speed;
        }
    }
}
