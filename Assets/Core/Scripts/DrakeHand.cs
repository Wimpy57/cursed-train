using System;
using Core.Scripts;
using Core.Scripts.EnemyStateMachine.MonsterStateMachine;
using UnityEngine;

public class DrakeHand : MonoBehaviour
{
    [SerializeField] private float CooldownTimer;
    [SerializeField] private Monster Drake;

    public float Timer { get; private set; }
    
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Camera") || Timer > 0 
            || Drake.CurrentState.GetType() != typeof(MonsterAttackState))
        {
            return;
        }
        Player.Instance.Hurt(Drake.GetDamage());
        Timer = CooldownTimer;
    }

    public void SetInitialTimerValue()
    {
        Timer = CooldownTimer;
    }

    private void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;
        }
    }
}
