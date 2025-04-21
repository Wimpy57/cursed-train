using UnityEngine;

namespace Core.Scripts.EnemyStateMachine
{
    public class AggressedState : EnemyState
    {
        public override void Behave(Enemy enemyContext)
        {
            enemyContext.Agent.destination = Player.Instance.transform.position;
            //if ()
        }

        protected override void Transition(Enemy enemyContext)
        {
            
        }
    }
    
}
