using System;
using System.Collections;
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
        [Header("Heart beat parameters")]
        [SerializeField] private Animator _heartBeatAnimator;
        [SerializeField] private int _normalHpPercentLimit;
        [SerializeField] private int _lowHpPercentLimit;
        [SerializeField] private int _criticalHpPercentLimit;

        [Header("Metal material parameters")] 
        [SerializeField] private Material _metalMaterial;
        [SerializeField] private Color _defaultMetalMaterialColor;
        [SerializeField] private Color _criticalMetalMaterialColor;
        [SerializeField] private Color _dataStoredMetalMaterialColor;
        [SerializeField] private float _dataStoringEffectDuration;

        private bool _isDataStoring;
        
        
        private void Start()
        {
            _metalMaterial.color = _defaultMetalMaterialColor;
            _hpText.text = _defaultText + Player.Instance.MaxHp;
            if (Player.Instance == null)
            {
                _hpText.text = _defaultText;
            }
            
            _heartBeatAnimator.speed = .15f;
            
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
                case State.FindTheKey:
                    _questImageIcon.sprite = _questIcons.FindTheKeyQuestIcon;
                    break;
                case State.OpenTheDoor:
                    _questImageIcon.sprite = _questIcons.OpenTheDoorQuestIcon;
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
            
            if (Player.Instance.Hp <= Player.Instance.MaxHp * ((float) _criticalHpPercentLimit / 100))
            {
                _heartBeatAnimator.speed = .8f;
            }
            else if (Player.Instance.Hp <=  Player.Instance.MaxHp * ((float) _lowHpPercentLimit / 100))
            {
                _heartBeatAnimator.speed = .45f;
            }
            else if (Player.Instance.Hp <=  Player.Instance.MaxHp * ((float) _normalHpPercentLimit / 100))
            {
                _heartBeatAnimator.speed = .3f;
            }
            _metalMaterial.color = Color.Lerp(_defaultMetalMaterialColor, _criticalMetalMaterialColor,  
                1f - (float) Player.Instance.Hp / Player.Instance.MaxHp);
        }

        public void StoreDataVisual()
        {
            StartCoroutine(StoreData());
        }

        private IEnumerator StoreData()
        {
            if (_isDataStoring) yield break;
            
            _isDataStoring = true;
            float elapsedTime = 0f;
            Color defaultColor = _metalMaterial.color;
            
            while (_metalMaterial.color != _dataStoredMetalMaterialColor)
            {
                elapsedTime += Time.deltaTime;
                Color color = Color.Lerp(defaultColor, _dataStoredMetalMaterialColor, elapsedTime/_dataStoringEffectDuration);
                _metalMaterial.color = color;
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            
            elapsedTime = 0f;
            while (_metalMaterial.color != defaultColor)
            {
                elapsedTime += Time.deltaTime;
                Color color = Color.Lerp(_dataStoredMetalMaterialColor, defaultColor, elapsedTime/_dataStoringEffectDuration);
                _metalMaterial.color = color;
                yield return null;
            }

            _isDataStoring = false;
        }

        private void OnDisable()
        {
            _metalMaterial.color = _defaultMetalMaterialColor;
            if (Player.Instance == null) return;
            Player.Instance.OnHpChanged -= Player_OnHpChanged;
            StateManager.Instance.OnStateChanged -= StateManager_OnStateChanged;

        }
    }
}
