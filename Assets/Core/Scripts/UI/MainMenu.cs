using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts.UI
{
    public class MainMenu : MonoBehaviour, IStateChanger
    {

        [SerializeField] private GameObject _mainMenuPanel;
        [SerializeField] private GameObject _settingsPanel;
        [SerializeField] private Door _coupeDoor;
        
        private void Start()
        {
            if (StateManager.Instance.CurrentState != State.Menu)
            {
                Destroy(gameObject);
                return;
            }
            _mainMenuPanel.SetActive(true);
            _settingsPanel.SetActive(false);
            _coupeDoor.Close();
            _coupeDoor.Lock();
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

        public void Quit()
        {
            Application.Quit();
        }
    }
}
