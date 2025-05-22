using System;
using Core.Scripts.States;
using UnityEngine;

namespace Core.Scripts
{
    public class SnapSocket : MonoBehaviour
    {
        [SerializeField] private GameObject _snappableObjectPrefab;
        [SerializeField] private Material _snapSocketMaterial;
        [SerializeField] private float _maxAlpha;
        [SerializeField] private bool _storesObjectsBetweenScenes;

        public static event EventHandler OnObjectUnsnapped;

        private bool _isObjectSnapped;
        
        private void Start()
        {
            Color color = _snapSocketMaterial.color;
            color.a = 0f;
            _snapSocketMaterial.color = color;
            
            if (StateManager.Instance.WasSnapped && _storesObjectsBetweenScenes)
            {
                SnappableObject snappableObject = Instantiate(_snappableObjectPrefab).GetComponent<SnappableObject>();
                SnapObject(snappableObject);
            }
        }

        public virtual void SnapObject(SnappableObject snappableObject)
        {
            _isObjectSnapped = true;
            snappableObject.SetSnapSocket(this);
        }

        private void UnSnapObject(SnappableObject snappableObject)
        {
            OnObjectUnsnapped?.Invoke(this, EventArgs.Empty);
            snappableObject.SetSnapSocket(null);
            _isObjectSnapped = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.CompareTag("SnappableObject")) return;
            if (_isObjectSnapped) return;
            
            SnappableObject snappableObject = other.gameObject.GetComponent<SnappableObject>();
            if (snappableObject.TryGetSnapSocket(out var socket))
            {
                socket.UnSnapObject(snappableObject);
            }
            
            SnapObject(snappableObject);
        }
    }
}
