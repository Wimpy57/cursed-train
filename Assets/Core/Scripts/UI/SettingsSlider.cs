using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    public class SettingsSlider : MonoBehaviour
    {
        [SerializeField] private bool _syncWithVolume;
        [SerializeField] private float _defaultValue;
        
        private Slider _slider;
        
        private void Start()
        {
            _slider = GetComponent<Slider>();
            if (_syncWithVolume)
            {
                _slider.value = AudioManager.Instance.GetVolume();
                return;
            }
            _slider.value = _defaultValue;
        }
    }
}
