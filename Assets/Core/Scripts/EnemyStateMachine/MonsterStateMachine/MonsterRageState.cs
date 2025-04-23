namespace Core.Scripts.EnemyStateMachine.MonsterStateMachine
{
    public class MonsterRageState : EnemyState
    {
        public override void Behave(Enemy enemyContext)
        {
            // todo rage state behaviour 
            // behaves without transition to other states and dies at the end
        }

        public override void Enter(Enemy enemyContext)
        {
            //todo play animation and change parameters
        }
    }
}