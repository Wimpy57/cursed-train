using System;
using UnityEngine;

namespace Core.Scripts.UI
{
    public class HeartBeatAnimationSpeed : MonoBehaviour
    {
        [SerializeField] private int _normalHpLimit;
        [SerializeField] private int _lowHpLimit;
        [SerializeField] private int _criticalHpLimit;
        
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _animator.speed = 0f;
            
            Player.Instance.OnHpChanged += Player_OnHpChanged;
        }

        private void Player_OnHpChanged(object sender, EventArgs e)
        {
            
            if (Player.Instance.Hp < _criticalHpLimit)
            {
                _animator.speed = 1f;
            }
            else if (Player.Instance.Hp < _lowHpLimit)
            {
                _animator.speed = .5f;
            }
            else if (Player.Instance.Hp < _normalHpLimit)
            {
                _animator.speed = .3f;
            }
        }
    }
}
