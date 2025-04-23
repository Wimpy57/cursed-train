namespace Core.Scripts.EnemyStateMachine.MonsterStateMachine
{
    public class MonsterChaseState : AggressedState
    {
        public override void Behave(Enemy enemyContext)
        {
            float distanceToPlayer = GetDistance(enemyContext.transform, Player.Instance.transform);
            if (distanceToPlayer > enemyContext.DistanceToAggress)
            {
                enemyContext.ChangeState(new MonsterIdleState());
            }

            if (distanceToPlayer <= enemyContext.DistanceToAttack)
            {
                enemyContext.ChangeState(new MonsterAttackState());
            }
        }
    }
}