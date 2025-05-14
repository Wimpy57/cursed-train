using System;
using TMPro;
using UnityEngine;

namespace Core.Scripts.UI
{
    public class WristMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _hpText;
        [SerializeField] private string _defaultText;
        
        private void Start()
        {
            _hpText.text = _defaultText + Player.Instance.MaxHp;
            if (Player.Instance == null)
            {
                _hpText.text = _defaultText;
            }
            Player.Instance.OnHpChanged += Player_OnHpChanged;
        }

        private void Player_OnHpChanged(object sender, EventArgs e)
        {
            _hpText.text = _defaultText + Player.Instance.Hp;
        }

        private void OnDisable()
        {
            Player.Instance.OnHpChanged -= Player_OnHpChanged;
        }
    }
}
