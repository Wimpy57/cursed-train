using Core.Scripts.Doors;
using UnityEngine;

namespace Core.Scripts
{
    public class WristKeyReader : MonoBehaviour
    {
        [SerializeField] private bool _isOpenable;
        [SerializeField] private Door _door;
        
        private void OnTriggerEnter(Collider other)
        {
            bool isSuccess = _isOpenable;
            
            if (!other.gameObject.CompareTag("Watch")) return;
            if (!Player.Instance.IsKeyDataStored())
            {
                isSuccess = false;
            }
            
            Player.Instance.OperateWithWatchData(isSuccess);
            
            if (_isOpenable)
            {
                _door.Unlock();
            }
        }
    }
}
