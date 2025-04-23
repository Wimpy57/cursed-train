namespace Core.Scripts.EnemyStateMachine.SpiderStateMachine
{
    public class SpiderDisappearState : EnemyState
    {
        public override void Behave(Enemy enemyContext)
        {
            // todo move through direction and disappear at the end
        }

        public override void Enter(Enemy enemyContext)
        {
            // todo get vector to player's transform and move through it
        }
    }
}