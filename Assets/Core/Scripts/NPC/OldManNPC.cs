using System;
using System.Collections;
using Core.Scripts.Achievements;
using Core.Scripts.States;
using TMPro;
using UnityEngine;

namespace Core.Scripts.NPC
{
    public class OldManNpc : Npc, IStateChanger
    {
        [Header("Old Man parameters")]
        [SerializeField] private bool _updateStateAfterSpeech = true;
        [SerializeField] private State _speechState;
        [SerializeField] private Animator _animator;

        private bool _wasAchievementSpawned;
        
        protected override void Start()
        {
            base.Start();
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
        }

        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            if (StateManager.Instance.CurrentState == _speechState)
            {
                StartCoroutine(Speak(SpeechDataList));
            }
        }

        protected override IEnumerator Speak(SpeechData[] speechDataList)
        {
            _animator.SetBool("IsTalking", true);
            yield return StartCoroutine(base.Speak(speechDataList));
            _animator.SetBool("IsTalking", false);
            
            if (_updateStateAfterSpeech)
            {
                StateManager.Instance.UpgradeState(this);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Extinguisher") && !_wasAchievementSpawned)
            {
                AchievementManager.Instance.GenerateAchievement(Achievement.Inadequate);
                _wasAchievementSpawned = true;
            }
        }
        
        private void OnDisable()
        {
            StateManager.Instance.OnStateChanged -= StateManager_OnStateChanged;
        }
    }
}
