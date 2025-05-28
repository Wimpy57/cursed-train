using UnityEngine;

namespace Core.Scripts.EnemyStateMachine
{
    public class ChaseState : EnemyState
    {
        public override void Behave(Enemy enemyContext)
        {
            enemyContext.SetDestination(Player.Instance.transform.position);
            float distanceToPlayer = GetDistance(enemyContext.transform, Player.Instance.transform);
            
            if (distanceToPlayer > enemyContext.DistanceToAggress)
            {
                enemyContext.ChangeState(new IdleState());
            }

            if (distanceToPlayer <= enemyContext.DistanceToAttack)
            {
                enemyContext.ChangeState(new AttackState());
            }
        }

        public override void Enter(Enemy enemyContext)
        {
            enemyContext.SetSpeed(enemyContext.ChaseStateSpeed);
		
            enemyContext.EnemyAnimator.SetBool("IsAgro", false);
            enemyContext.EnemyAnimator.SetBool("IsChasing", true);
        }
    }
    
}
