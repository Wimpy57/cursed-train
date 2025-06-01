

namespace Core.Scripts.EnemyStateMachine.MonsterStateMachine
{
    public class MonsterDieState : EnemyState
    {
        public override void Behave(Enemy enemyContext)
        {
            
        }
        
        public override void Enter(Enemy enemyContext)
        {
            enemyContext.SetSpeed(0f);
        }
    }
}