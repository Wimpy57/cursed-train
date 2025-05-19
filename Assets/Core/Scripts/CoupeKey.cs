using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts
{
    public class CoupeKey : MonoBehaviour, IStateChanger
    {
        [SerializeField] private State _stateWhenCanUpgrade;
        

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Watch")) return;
            Player.Instance.StoreData();

            if (StateManager.Instance.CurrentState != _stateWhenCanUpgrade) return;
            StateManager.Instance.UpgradeState(this);
        }
    }
}
