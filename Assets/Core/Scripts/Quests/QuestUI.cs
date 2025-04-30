using System;
using Core.Scripts.States;
using TMPro;
using UnityEngine;

namespace Core.Scripts.Quests
{
    public class QuestUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _questText;

        //todo hide and show quests using controller button
        
        private void Start()
        {
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
            SetText();
        }

        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            SetText();
        }

        private void SetText()
        {
            _questText.text = QuestInfo.QuestByState[StateManager.Instance.CurrentState];
        }

        private void OnDisable()
        {
            StateManager.Instance.OnStateChanged -= StateManager_OnStateChanged;
        }
    }
}
