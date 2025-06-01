using System;
using System.Collections;
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
        [SerializeField] private bool _updateStateAfterSpeech = true;
        [Header("Speech")]
        [SerializeField] private State _speechState;
        [SerializeField] private SpeechData[] _speechData;
        
        private AudioSource _audioSource;
        private bool _wasAchievementSpawned;

        [Serializable]
        private struct SpeechData
        {
            public AudioClip Clip;
            public string Phrase;
            public float DelayAfterPhrase;
        }
        
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
            foreach (var item in _speechData)
            {
                float timeToWait = 5f;
                if (item.Clip)
                {
                    _audioSource.PlayOneShot(item.Clip, AudioManager.Instance.GetVolume());
                    timeToWait = item.Clip.length;
                }
                DisplayPhrase(item.Phrase);
                yield return new WaitForSeconds(timeToWait);
                yield return new WaitForSeconds(item.DelayAfterPhrase);
                RemovePhrase();
            }
            
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
