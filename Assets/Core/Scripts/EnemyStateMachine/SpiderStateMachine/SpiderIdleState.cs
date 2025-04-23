namespace Core.Scripts.EnemyStateMachine.SpiderStateMachine
{
    public class SpiderIdleState : IdleState
    {
        public override void Behave(Enemy enemyContext)
        {
            float distanceToPlayer = GetDistance(enemyContext.transform, Player.Instance.transform);
            if (distanceToPlayer <= enemyContext.DistanceToAggress)
            {
                enemyContext.ChangeState(new SpiderChaseState());
            }
        }
    }
}