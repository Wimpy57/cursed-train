using UnityEngine;

namespace Core.Scripts.EnemyStateMachine
{
    public abstract class EnemyState
    {
        public abstract void Behave(Enemy enemyContext);
        
        public abstract void Enter(Enemy enemyContext);
        
        protected float GetDistance(Transform enemy, Transform target)
        {
            return (enemy.position - target.position).magnitude;
        }
    }
}
