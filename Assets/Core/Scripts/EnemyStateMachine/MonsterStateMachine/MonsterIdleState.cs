namespace Core.Scripts.EnemyStateMachine.MonsterStateMachine
{
    public class MonsterIdleState : IdleState
    {
        public override void Behave(Enemy enemyContext)
        {
            float distanceToPlayer = GetDistance(enemyContext.transform, Player.Instance.transform);
            if (distanceToPlayer <= enemyContext.DistanceToAggress)
            {
                enemyContext.ChangeState(new MonsterChaseState());
            }
        }
    }
}