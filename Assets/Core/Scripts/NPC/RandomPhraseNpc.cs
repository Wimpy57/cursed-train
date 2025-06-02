using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Scripts.NPC
{
    public class RandomPhraseNpc : Npc
    {
        [SerializeField] private SpeechData[] _secondSpeechDataList;
        [SerializeField] private SpeechData[] _thirdSpeechDataList;
        [SerializeField] private SpeechData[] _forthSpeechDataList;
        
        private List<SpeechData[]> _speech;
        private int _previousSpeechIndex = -1;
        
        protected override void Start()
        {
            _speech = new List<SpeechData[]>()
            {
                SpeechDataList,
                _secondSpeechDataList,
                _thirdSpeechDataList,
                _forthSpeechDataList,
            };
            
            base.Start();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            if (IsSpeaking) return;
            ChooseRandomSpeech();
        }

        private void ChooseRandomSpeech()
        {
            int choice;
            while (true)
            {
                choice = Random.Range(0, _speech.Count);
                if (_previousSpeechIndex == -1) break;
                if (choice != _previousSpeechIndex) break;
            }
            
            _previousSpeechIndex = choice;
            StartCoroutine(Speak(_speech[choice]));
        }
    }
}