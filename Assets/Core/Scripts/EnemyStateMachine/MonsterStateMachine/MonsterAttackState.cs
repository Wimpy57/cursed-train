using UnityEngine;

namespace Core.Scripts.EnemyStateMachine.MonsterStateMachine
{
    public class MonsterAttackState : AttackState
    {

        private float _currentHitInterval;
        private float _timeToHit;
        
        public override void Behave(Enemy enemyContext)
        {
            float distanceToPlayer = GetDistance(enemyContext.transform, Player.Instance.transform);
            if (distanceToPlayer >= enemyContext.DistanceToAttack)
            {
                enemyContext.ChangeState(new MonsterChaseState());
            }
        }
        
        
    }
}
