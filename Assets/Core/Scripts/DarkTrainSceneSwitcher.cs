using System;
using System.Collections;
using Core.Scripts.Scenes;
using Core.Scripts.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scripts
{
    
    public class DarkTrainSceneSwitcher : MonoBehaviour
    {
        [SerializeField] private float _timeToFade;
        
        private void Start()
        {
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
        }

        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            if (StateManager.Instance.CurrentState == State.DarkNewTrain)
            {
                StartCoroutine(SwitchToDarkTrain());
            }
        }

        private IEnumerator SwitchToDarkTrain()
        {
            //todo visual
            yield return StartCoroutine(Player.Instance.Fade(_timeToFade));
            SceneManager.LoadScene(SceneInfo.SceneStringNameDictionary[SceneName.DarkNewTrainScene]);
            yield return null;
        }
        
        private void OnDisable()
        {
            StateManager.Instance.OnStateChanged -= StateManager_OnStateChanged;
        }
    }
}
