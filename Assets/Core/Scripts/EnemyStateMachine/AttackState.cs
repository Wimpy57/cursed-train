namespace Core.Scripts.EnemyStateMachine
{
    public class AttackState : EnemyState
    {
        public override void Behave(Enemy enemyContext)
        {
            float distanceToPlayer = GetDistance(enemyContext.transform, Player.Instance.transform);
            if (distanceToPlayer >= enemyContext.DistanceToAttack)
            {
                enemyContext.ChangeState(new ChaseState());
            }
        }

        public override void Enter(Enemy enemyContext)
        {
            enemyContext.SetSpeed(0f);
            
            enemyContext.EnemyAnimator.SetBool("IsAgro", true);
            enemyContext.EnemyAnimator.SetBool("IsChasing", false);
        }
    }
}
