namespace Core.Scripts.EnemyStateMachine
{
    public class IdleState : EnemyState
    {
        public override void Behave(Enemy enemyContext)
        {
            float distanceToPlayer = GetDistance(enemyContext.transform, Player.Instance.transform);
            if (distanceToPlayer <= enemyContext.DistanceToAggress)
            {
                enemyContext.ChangeState(new ChaseState());
            }
        }

        public override void Enter(Enemy enemyContext)
        {
            enemyContext.SetSpeed(0f);
        }
    }
}
