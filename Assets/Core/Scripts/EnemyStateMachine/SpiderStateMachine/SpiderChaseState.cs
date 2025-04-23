namespace Core.Scripts.EnemyStateMachine.SpiderStateMachine
{
    public class SpiderChaseState : AggressedState
    {
        public override void Behave(Enemy enemyContext)
        {
            float distanceToPlayer = GetDistance(enemyContext.transform, Player.Instance.transform);
            
            if (distanceToPlayer <= enemyContext.DistanceToAttack)
            {
                enemyContext.ChangeState(new SpiderDisappearState());
            }
        }
    }
}