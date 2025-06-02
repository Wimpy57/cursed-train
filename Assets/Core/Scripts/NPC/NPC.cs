using System;
using System.Collections;
using Core.Scripts.States;
using TMPro;
using UnityEngine;

namespace Core.Scripts.NPC
{
    public class Npc : MonoBehaviour
    {
        [Header("Npc parameters")] 
        [SerializeField] private GameObject _subtitleCanvas;
        [SerializeField] private TextMeshProUGUI _subtitleText;
        [SerializeField] protected SpeechData[] SpeechDataList;
        
        [Serializable]
        protected struct SpeechData
        {
            public AudioClip Clip;
            public string Phrase;
            public float DelayAfterPhrase;
        }

        protected bool IsSpeaking;

        private AudioSource _audioSource;
        
        protected virtual void Start()
        {
            _audioSource = GetComponent<AudioSource>();
            RemovePhrase();
        }
        
        protected virtual IEnumerator Speak(SpeechData[] speechDataList)
        {
            IsSpeaking = true;
            foreach (var item in speechDataList)
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
            IsSpeaking = false;
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
    }
}