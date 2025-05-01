using Core.Scripts.EnemyStateMachine.MonsterStateMachine;
using UnityEngine;

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
