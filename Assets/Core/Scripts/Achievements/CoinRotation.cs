using UnityEngine;
using UnityEngine.Serialization;

namespace Core.Scripts.Achievements
{
    public class CoinRotation : MonoBehaviour
    {
        [SerializeField] private float _amplitude;
        [SerializeField] private float _movementSpeed;
        [SerializeField] private float _degreesPerSecond;

        private float _timer;
        private Vector3 _startPosition;
        
        
        private void Start()
        {
            _startPosition = transform.position;
        }

        private void Update()
        {
            transform.Rotate(Vector3.up * (_degreesPerSecond * Time.deltaTime), Space.World);

            _timer += Time.deltaTime * _movementSpeed;
            transform.position = _startPosition + Vector3.up * (_amplitude * Mathf.Sin(_timer));
        }
    }
}
