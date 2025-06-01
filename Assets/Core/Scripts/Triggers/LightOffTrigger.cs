using System.Collections;
using Core.Scripts.Achievements;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class LightOffTrigger : EmptyTrigger
    {
        [SerializeField] private float _lightOffDuration;
        [SerializeField] private Light[] _lights;
        [SerializeField] private bool _isTrainHornScreamer;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            StartCoroutine(LightOff());
        }

        private IEnumerator LightOff()
        {
            foreach (var item in _lights)
            {
                item.enabled = false;
            }
            yield return new WaitForSeconds(_lightOffDuration);
            foreach (var item in _lights)
            {
                item.enabled = true;
            }

            if (_isTrainHornScreamer)
            {
                AchievementManager.Instance.GenerateAchievement(Achievement.AreYouScared);
            }
        }
    }
}