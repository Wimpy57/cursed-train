using UnityEngine;
using Core.Scripts.EnemyStateMachine;
using UnityEngine.AI;

namespace Core.Scripts
{
    public abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected int Hp;
        [SerializeField] protected int Damage;
        
        [Header("Enemy states settings")]
        [SerializeField] public float DistanceToAggress;
        [SerializeField] public float DistanceToAttack;
        [SerializeField] public float ChaseStateSpeed;
        [SerializeField] public int HpToRage;
        [SerializeField] public float HitIntervalMin;
        [SerializeField] public float HitIntervalMax;
        [SerializeField] public float RageHitInterval;
        [SerializeField] public float RageLifetime;

        [Header("Enemy animation")] 
        [SerializeField] public Animator EnemyAnimator;
        
        private NavMeshAgent _agent;
        
        protected EnemyState CurrentState;
        
        protected void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Extinguisher")) return;
            
            Hp--;
            if (Hp <= 0)
            {
                Die();
            }
        }

        protected virtual void Awake()
        {
            CurrentState = new IdleState();    
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
            CurrentState.Behave(this);
        }

        public void ChangeState(EnemyState state)
        {
            CurrentState = state;
            CurrentState.Enter(this);
        }

        public void SetDestination(Vector3 destination)
        {
            _agent.SetDestination(destination);
        }

        public void SetSpeed(float speed)
        {
            _agent.speed = speed;
        }

        public void Die()
        {
            Destroy(gameObject);
        }

        public int GetHp() => Hp;
        
        public int GetDamage() => Damage;
    }
}
