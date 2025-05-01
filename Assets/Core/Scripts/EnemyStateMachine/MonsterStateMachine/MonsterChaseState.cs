namespace Core.Scripts.EnemyStateMachine.MonsterStateMachine
{
    public class MonsterChaseState : ChaseState
    {
        public override void Behave(Enemy enemyContext)
        {
            enemyContext.SetDestination(Player.Instance.transform.position);
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
