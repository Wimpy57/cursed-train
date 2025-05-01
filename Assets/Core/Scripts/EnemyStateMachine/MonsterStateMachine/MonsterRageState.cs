using UnityEngine;

namespace Core.Scripts.EnemyStateMachine.MonsterStateMachine
{
    public class MonsterRageState : EnemyState
    {
        private float _lifetime;
        private float _intervalToHit;
        private float _timeToHit;
        
        public override void Behave(Enemy enemyContext)
        {
            _lifetime -= Time.deltaTime;
            if (_lifetime <= 0)
            {
                enemyContext.Die();
            }

            HandleFight(enemyContext);
        }

        public override void Enter(Enemy enemyContext)
        {
            _lifetime = enemyContext.RageLifetime;
            _intervalToHit = enemyContext.RageHitInterval;
        }

        private void HandleFight(Enemy enemyContext)
        {
            if (_timeToHit <= 0)
            {
                Player.Instance.Hurt(enemyContext.GetDamage());
                _timeToHit = _intervalToHit;
                return;
            }
            _timeToHit -= Time.deltaTime;
        }
    }
}