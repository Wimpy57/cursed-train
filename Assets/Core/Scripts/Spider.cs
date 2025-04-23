using Core.Scripts.EnemyStateMachine.SpiderStateMachine;
using UnityEngine;

namespace Core.Scripts
{
    public class Spider : Enemy
    {
        protected override void Awake()
        {
            CurrentState = new SpiderIdleState();
        }
    }
}
