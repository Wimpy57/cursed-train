using System;
using System.Collections;
using Core.Scripts.Scenes;
using Core.Scripts.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scripts
{
    
    public class FinalSceneSwitcher : MonoBehaviour
    {
        [SerializeField] private float _timeToFade;
        
        private void Start()
        {
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
        }

        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            if (StateManager.Instance.CurrentState == State.Final)
            {
                StartCoroutine(SwitchToFinal());
            }
        }

        private IEnumerator SwitchToFinal()
        {
            //todo visual
            yield return StartCoroutine(Player.Instance.Fade(_timeToFade));
            SceneManager.LoadScene(SceneInfo.SceneStringNameDictionary[SceneName.FinalScene]);
            yield return null;
        }
        
        private void OnDisable()
        {
            StateManager.Instance.OnStateChanged -= StateManager_OnStateChanged;
        }
    }
}
