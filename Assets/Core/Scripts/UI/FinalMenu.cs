using System;
using System.Collections;
using Core.Scripts.Achievements;
using Core.Scripts.Scenes;
using Core.Scripts.States;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    public class FinalMenu : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject _defaultPanel;
        [SerializeField] private GameObject _achievementPanel;

        [Header("Default panel counters")] 
        [SerializeField] private TextMeshProUGUI _timerResultText;
        [SerializeField] private TextMeshProUGUI _monstersKilledAmountText;
        [SerializeField] private TextMeshProUGUI _hpLostAmountText;

        [Header("Buttons")] 
        [SerializeField] private Button _seeAchievementsButton;
        [SerializeField] private Button _backToMenuButton;
        [SerializeField] private Button _backToDefaultButton;

        [Header("Achievements")] 
        [SerializeField] private GameObject _achievementList;
        [SerializeField] private GameObject _achievementPrefab;

        private bool _isMenuLoading;
        
        private void Start()
        {
            _seeAchievementsButton.onClick.AddListener(SeeAchievements);
            _backToMenuButton.onClick.AddListener(BackToMenu);
            _backToDefaultButton.onClick.AddListener(BackToDefaultPanel);
            BackToDefaultPanel();
            InitializeGameInfo();
            InitializeAchievementList();
        }

        private void SeeAchievements()
        {
            _defaultPanel.SetActive(false);
            _achievementPanel.SetActive(true);
        }

        private void BackToMenu()
        {
            if (_isMenuLoading) return;
            StartCoroutine(LoadMenuScene());
        }

        private void BackToDefaultPanel()
        {
            _achievementPanel.SetActive(false);
            _defaultPanel.SetActive(true);
        }

        private void InitializeGameInfo()
        {
            float timeSpent = Timer.Instance.GetTime();
            string timeSpentString = $"{(int) timeSpent / 60} мин. {(int)(timeSpent % 60)} сек.";
            
            _hpLostAmountText.text = StateManager.Instance.TotalHpLost.ToString();
            _monstersKilledAmountText.text = StateManager.Instance.TotalMonstersKilled.ToString();
            _timerResultText.text = timeSpentString;
        }

        private void InitializeAchievementList()
        {
            foreach (Achievement achievement in Enum.GetValues(typeof(Achievement)))
            {
                AchievementUI achievementUI = Instantiate(_achievementPrefab, _achievementList.transform)
                    .GetComponent<AchievementUI>();
                achievementUI.Initialize(achievement);
            }
        }

        private IEnumerator LoadMenuScene()
        {
            _isMenuLoading = true;
            StateManager.Instance.Restart();
            yield return StartCoroutine(Player.Instance.Fade(1.5f));
            SceneManager.LoadScene(SceneInfo.SceneStringNameDictionary[SceneName.NewTrainScene]);
        }
    }
}
