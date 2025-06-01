using System;
using Core.Scripts;
using UnityEngine;

public class DrakeHand : MonoBehaviour
{
    [SerializeField] private float CooldownTimer;

    public float Timer { get; private set; }
    
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Camera") || Timer > 0)
        {
            return;
        }
        Player.Instance.Hurt(20);
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
