namespace Core.Scripts.EnemyStateMachine.MonsterStateMachine
{
    public class MonsterAttackState : AttackState
    {
        public override void Behave(Enemy enemyContext)
        {
            float distanceToPlayer = GetDistance(enemyContext.transform, Player.Instance.transform);
            if (distanceToPlayer >= enemyContext.DistanceToAttack)
            {
                enemyContext.ChangeState(new MonsterChaseState());
            }

            if (enemyContext.GetHp() == enemyContext.HpToRage)
            {
                enemyContext.ChangeState(new MonsterRageState());
            }
        }
    }
}