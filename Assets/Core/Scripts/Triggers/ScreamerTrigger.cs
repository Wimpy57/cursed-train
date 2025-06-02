using System.Collections;
using UnityEngine;

namespace Core.Scripts.Triggers
{
    public class ScreamerTrigger : EmptyTrigger
    {
        [SerializeField] private float ScreamerDuration;
        [SerializeField] private GameObject Screamer;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            StartCoroutine(Scare());
        }

        private IEnumerator Scare()
        {
            Screamer.SetActive(true);
            yield return new WaitForSeconds(ScreamerDuration);
            Screamer.SetActive(false);
            }
        }
    }
