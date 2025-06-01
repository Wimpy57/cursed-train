using System;
using System.Collections;
using Core.Scripts.Scenes;
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
        [Header("Pause")] 
        [SerializeField] private GameObject _pauseCanvas;
        [SerializeField] private GameObject _pausePanel;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _quitButton;
        [SerializeField] private Button _mainMenuButton;
        [Header("Settings")]
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private Button _volumeSettingsButton;
        [SerializeField] private Button _graphicsSettingsButton;
        [SerializeField] private Button _backToPauseButton;
        [Header("Volume")]
        [Header("Graphics")]
        [Header("Heart beat parameters")]
        [SerializeField] private Animator _heartBeatAnimator;
        [SerializeField] private int _normalHpPercentLimit;
        [SerializeField] private int _lowHpPercentLimit;
        [SerializeField] private int _criticalHpPercentLimit;
        [Header("Screen material parameters")] 
        [SerializeField] private Material _screenMaterial;
        [SerializeField] private Light _screenLight;
        [SerializeField] private Color _defaultScreenMaterialColor;
        [SerializeField] private Color _criticalScreenMaterialColor;
        [SerializeField] private Color _dataStoredScreenMaterialColor;
        [SerializeField] private Color _wrongDataScreenMaterialColor;
        [SerializeField] private float _dataStoringEffectDuration;

        private bool _isDataStoring;
        private bool _isMenuLoading;
        
        private void Start()
        {
            _screenLight.intensity = 0f;
            _screenLight.color = _criticalScreenMaterialColor;
            
            _hpText.text = _defaultText + Player.Instance.MaxHp;
            if (Player.Instance == null)
            {
                _hpText.text = _defaultText;
            }
            
            _heartBeatAnimator.speed = .15f;
            SetQuestIcon();
            //
            // _settingsButton.onClick.AddListener(OpenSettingsPanel);
            // _quitButton.onClick.AddListener(ClosePauseCanvas);
            // _mainMenuButton.onClick.AddListener(BackToMenu);
            // _backToPauseButton.onClick.AddListener(OpenPauseCanvas);
            
        }

        private void OnEnable()
        {
            StartCoroutine(SubscribeOnEnable());
        }

        private IEnumerator SubscribeOnEnable()
        {
            while (Player.Instance == null || StateManager.Instance == null)
            {
                yield return new WaitForEndOfFrame();
            }
            Player.Instance.OnHpChanged += Player_OnHpChanged;
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
            
        }

        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
           SetQuestIcon();
        }

        private void SetQuestIcon()
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

            float percentage = 1f - (float)Player.Instance.Hp / Player.Instance.MaxHp;
            _screenMaterial.color = Color.Lerp(_defaultScreenMaterialColor, _criticalScreenMaterialColor, percentage);
            _screenLight.color = _criticalScreenMaterialColor;
            _screenLight.intensity = Mathf.Lerp(0f, 1f, 1f - (float) Player.Instance.Hp / Player.Instance.MaxHp);
        }

        public void IndicateWatchOperation(bool isSuccess)
        {
            Color color = isSuccess ? _dataStoredScreenMaterialColor : _wrongDataScreenMaterialColor;
            StartCoroutine(IndicateOperation(color));
        }

        private IEnumerator IndicateOperation(Color resultColor)
        {
            if (_isDataStoring) yield break;
            
            _isDataStoring = true;
            float elapsedTime = 0f;
            Color defaultColor = _screenMaterial.color;
            _screenLight.intensity = 1f;
            _screenLight.color = resultColor;
            while (_screenMaterial.color != resultColor)
            {
                elapsedTime += Time.deltaTime;
                _screenMaterial.color = Color.Lerp(defaultColor, resultColor, elapsedTime/_dataStoringEffectDuration);
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            
            elapsedTime = 0f;
            _screenLight.intensity = 0f;
            _screenLight.color = _criticalScreenMaterialColor;
            while (_screenMaterial.color != defaultColor)
            {
                elapsedTime += Time.deltaTime;
                _screenMaterial.color = Color.Lerp(resultColor, defaultColor, elapsedTime/_dataStoringEffectDuration);
                yield return null;
            }

            _isDataStoring = false;
        }

        public void OpenPauseCanvas()
        {
            SetPauseCanvasActive(true);
            SetPausePanelActive(true);
            SetSettingsActive(false);
        }
        
        private void ClosePauseCanvas()
        {
            SetPauseCanvasActive(false);
        }
        
        private void SetPauseCanvasActive(bool isActive)
        {
            _pauseCanvas.gameObject.SetActive(isActive);
        }
        
        private void SetPausePanelActive(bool isActive)
        {
            _pausePanel.gameObject.SetActive(isActive);
        }

        private void OpenSettingsPanel()
        {
            SetPausePanelActive(false);
            SetSettingsActive(true);
        }

        private void SetSettingsActive(bool isActive)
        {
            _settingsPanel.gameObject.SetActive(isActive);
        }
        
        private void BackToMenu()
        {
            if (_isMenuLoading) return;
            StartCoroutine(LoadMenuScene());
        }
        
        private IEnumerator LoadMenuScene()
        {
            _isMenuLoading = true;
            StateManager.Instance.Restart();
            yield return StartCoroutine(Player.Instance.Fade(1.5f));
            SceneChanger.Instance.LoadScene();
        }

        private void OnDisable()
        {
            _screenMaterial.color = _defaultScreenMaterialColor;
            if (Player.Instance == null) return;
            Player.Instance.OnHpChanged -= Player_OnHpChanged;
            StateManager.Instance.OnStateChanged -= StateManager_OnStateChanged;
        }
    }
}
