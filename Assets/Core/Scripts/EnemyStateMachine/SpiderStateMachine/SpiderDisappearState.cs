using UnityEngine;

namespace Core.Scripts.EnemyStateMachine.SpiderStateMachine
{
    public class SpiderDisappearState : EnemyState
    {
        private Vector3 _playerDirection;
        
        public override void Behave(Enemy enemyContext)
        {
            // todo move through direction and disappear at the end
        }

        public override void Enter(Enemy enemyContext)
        {
            _playerDirection = (Player.Instance.transform.position - enemyContext.transform.position).normalized;
        }
    }
}