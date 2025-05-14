using System;
using Core.Scripts.ScriptableObjects;
using Core.Scripts.States;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    public class WristMenu : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _hpText;
        [SerializeField] private string _defaultText;
        [SerializeField] private QuestIconsSO _questIcons;
        [SerializeField] private Image _questImageIcon;
        
        private void Start()
        {
            _hpText.text = _defaultText + Player.Instance.MaxHp;
            if (Player.Instance == null)
            {
                _hpText.text = _defaultText;
            }
            
            Player.Instance.OnHpChanged += Player_OnHpChanged;
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
        }

        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            switch (StateManager.Instance.CurrentState)
            {
                case State.Menu:
                    _questImageIcon.sprite = _questIcons.MenuQuestIcon;
                    break;
                case State.CoupeState:
                    _questImageIcon.sprite = _questIcons.CoupeQuestIcon;
                    break;
                case State.DarkNewTrain:
                    _questImageIcon.sprite = _questIcons.DarkNewTrainQuestIcon;
                    break;
                case State.OldManSpeech:
                    _questImageIcon.sprite = _questIcons.OldManSpeechIcon;
                    break;
                case State.FindLever:
                    _questImageIcon.sprite = _questIcons.FindLeverQuestIcon;
                    break;
                case State.OldTrain:
                    _questImageIcon.sprite = _questIcons.OldTrainQuestIcon;
                    break;
                case State.ChildDefence:
                    _questImageIcon.sprite = _questIcons.ChildDefenceQuestIcon;
                    break;
                case State.Final:
                    _questImageIcon.sprite = _questIcons.FinalQuestIcon;
                    break;
            }
        }

        private void Player_OnHpChanged(object sender, EventArgs e)
        {
            _hpText.text = _defaultText + Player.Instance.Hp;
        }

        private void OnDisable()
        {
            if (Player.Instance == null) return;
            Player.Instance.OnHpChanged -= Player_OnHpChanged;
        }
    }
}
