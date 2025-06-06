using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts
{
    public class CoupeKey : MonoBehaviour, IStateChanger
    {
        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("Watch")) return;
            Player.Instance.OperateWithWatchData(true, true);
        }
    }
}
