namespace Core.Scripts.EnemyStateMachine
{
    public class AggressedState : EnemyState
    {
        public override void Behave(Enemy enemyContext)
        {
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
            enemyContext.SetDestination(Player.Instance.transform.position);
            enemyContext.SetSpeed(enemyContext.AggressedStateSpeed);
        }
    }
    
}
