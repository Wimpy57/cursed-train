using Core.Scripts.Doors;
using Core.Scripts.States;
using TMPro;
using UnityEngine;

namespace Core.Scripts.UI
{
    public class MainMenu : MonoBehaviour, IStateChanger
    {
        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private Door _coupeDoor;
        [SerializeField] private TextMeshProUGUI _volumeValueText;
        
        private void Start()
        {
            if (StateManager.Instance.CurrentState != State.Menu)
            {
                Destroy(gameObject);
                return;
            }
            _mainMenuPanel.SetActive(true);
            _settingsPanel.SetActive(false);
        }
        
        public void StartGame()
        {
            StateManager.Instance.UpgradeState(this);
            _coupeDoor.Unlock();
            Destroy(gameObject);
        }

        public void Settings()
        {
            _mainMenuPanel.SetActive(false);
            _settingsPanel.SetActive(true);
        }

        public void BackToMenu()
        {
            _mainMenuPanel.SetActive(true);
            _settingsPanel.SetActive(false);
        }

        public void SetVolume(float volume)
        {
            AudioManager.Instance.SetVolume(volume);
            _volumeValueText.text = (int)(volume * 100) + "%";
        }

        public void Quit()
        {
            Application.Quit();
        }
    }
}
