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
        [Header("Watch interface")]
        [SerializeField] private TextMeshProUGUI _hpText;
        [SerializeField] private string _defaultText;
        [SerializeField] private QuestIconsSO _questIcons;
        [SerializeField] private Image _questImageIcon;
        [Space(10)]
        [Header("Heart beat parameters")]
        [SerializeField] private Animator _heartBeatAnimator;
        [SerializeField] private int _normalHpPercentLimit;
        [SerializeField] private int _lowHpPercentLimit;
        [SerializeField] private int _criticalHpPercentLimit;
        
        private void Start()
        {
            _hpText.text = _defaultText + Player.Instance.MaxHp;
            if (Player.Instance == null)
            {
                _hpText.text = _defaultText;
            }
            
            _heartBeatAnimator.speed = 0f;
            
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
            
            if (Player.Instance.Hp <= Player.Instance.MaxHp * (_criticalHpPercentLimit / 100))
            {
                _heartBeatAnimator.speed = 1f;
            }
            else if (Player.Instance.Hp <=  Player.Instance.MaxHp * (_lowHpPercentLimit / 100))
            {
                _heartBeatAnimator.speed = .5f;
            }
            else if (Player.Instance.Hp <=  Player.Instance.MaxHp * (_normalHpPercentLimit / 100))
            {
                _heartBeatAnimator.speed = .3f;
            }
        }

        private void OnDisable()
        {
            if (Player.Instance == null) return;
            Player.Instance.OnHpChanged -= Player_OnHpChanged;
        }
    }
}
