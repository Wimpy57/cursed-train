using UnityEngine;

namespace Core.Scripts.EnemyStateMachine
{
    public abstract class EnemyState
    {
        public abstract void Behave(Enemy enemyContext);

        protected abstract void Transition(Enemy enemyContext);
    }
}
