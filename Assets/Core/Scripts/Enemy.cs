using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Core.Scripts.EnemyStateMachine;
using Core.Scripts.EnemyStateMachine.MonsterStateMachine;
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
        [SerializeField] public float TimeToTakeHitCooldown;

        [Header("Enemy animation")] 
        [SerializeField] public Animator EnemyAnimator;

        [SerializeField] public GameObject Spine;
        [SerializeField] public GameObject Neck;
        [SerializeField] public GameObject Head;
        [SerializeField] public DrakeHand RightArm;

        [SerializeField] public AudioSource _hitAudio;

        public static event EventHandler OnMonsterKilled;
        
        private NavMeshAgent _agent;
        private bool _isDead;
        private bool _isTakingDamage;
        
        public EnemyState CurrentState;
        
        protected void OnCollisionEnter(Collision other)
        {
            if (!other.gameObject.CompareTag("Extinguisher") || _isTakingDamage || _isDead) return;
            StartCoroutine(TakeHitCooldown());
            _hitAudio.Play();
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

        private IEnumerator TakeHitCooldown()
        {
            _isTakingDamage = true;
            yield return new WaitForSeconds(TimeToTakeHitCooldown);
            _isTakingDamage = false;
        }

        private void LateUpdate()
        {
            if (_isDead) return;
            if ( RightArm.Timer > 0.2f || CurrentState is not MonsterAttackState)
            {
                Vector3 SpineLookAt = Camera.main.transform.position;
                Vector3 MonsterLookAt = SpineLookAt;
                Vector3 HeadLookAt = SpineLookAt;
                MonsterLookAt.y = transform.position.y;
                transform.LookAt(MonsterLookAt);
                SpineLookAt.y -= 1.9f;
                HeadLookAt.y -= .7f;
                Head.transform.LookAt(HeadLookAt);
                SpineLookAt.Normalize();
                SpineLookAt.x = Spine.transform.forward.x;
                SpineLookAt.z = Spine.transform.forward.z;
                Spine.transform.forward = SpineLookAt;
            }
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

        public virtual void Die(bool destroyObject = true)
        {
            if (_isDead) return;
            
            _isDead = true;
            OnMonsterKilled?.Invoke(this, EventArgs.Empty);
            
            if (!destroyObject) return;
            Destroy(gameObject);
        }

        public int GetHp() => Hp;
        
        public int GetDamage() => Damage;
    }
}
