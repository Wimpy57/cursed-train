using System;
using System.Collections;
using System.Collections.Generic;
using Core.Scripts.Achievements;
using Core.Scripts.States;
using TMPro;
using UnityEngine;

namespace Core.Scripts
{
    public class OldManNpc : MonoBehaviour, IStateChanger
    {
        [Header("Parameters")] 
        [SerializeField] private GameObject _subtitleCanvas;
        [SerializeField] private TextMeshProUGUI _subtitleText;
        [Header("Speech")]
        [SerializeField] private State _speechState;
        [SerializeField] private List<AudioClip> _npcSpeechClips;
        [SerializeField] private List<string> _npcSpeechPhrases;
        
        private AudioSource _audioSource;
        private bool _wasAchievementSpawned;
        
        private void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            RemovePhrase();
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
        }

        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            if (StateManager.Instance.CurrentState == _speechState)
            {
                StartCoroutine(Speak());
            }
        }

        private IEnumerator Speak()
        {
            for (int i = 0; i < _npcSpeechPhrases.Count; i++)
            {
                float timeToWait = 5f;
                if (i < _npcSpeechClips.Count)
                {
                    _audioSource.PlayOneShot(_npcSpeechClips[i], AudioManager.Instance.GetVolume());
                    timeToWait = _npcSpeechPhrases[i].Length;
                }   
                DisplayPhrase(_npcSpeechPhrases[i]);
                yield return new WaitForSeconds(timeToWait);
                RemovePhrase();
            }
            StateManager.Instance.UpgradeState(this);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Extinguisher") && !_wasAchievementSpawned)
            {
                AchievementManager.Instance.GenerateAchievement(Achievement.Inadequate);
                _wasAchievementSpawned = true;
            }
        }

        private void DisplayPhrase(string phrase)
        {
            _subtitleText.text = phrase;
            _subtitleCanvas.SetActive(true);
        }

        private void RemovePhrase()
        {
            _subtitleText.text = "";
            _subtitleCanvas.SetActive(false);
        }
        
        private void OnDisable()
        {
            StateManager.Instance.OnStateChanged -= StateManager_OnStateChanged;
        }
    }
}
