    using Core.Scripts.EnemyStateMachine.MonsterStateMachine;

namespace Core.Scripts
{
    public class Monster : Enemy
    {
        
        protected override void Awake()
        {
            CurrentState = new MonsterIdleState();
        }
    }
}
