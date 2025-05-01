using System;
using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts
{
    public class DarkTrainLightManager : MonoBehaviour
    {
        private void Start()
        {
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
        }

        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            if (StateManager.Instance.CurrentState == State.DarkNewTrain){
                // going to change environment to dark
            }
        }

        private void OnDisable()
        {
            StateManager.Instance.OnStateChanged -= StateManager_OnStateChanged;
        }
    }
}
