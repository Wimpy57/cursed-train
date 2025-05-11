using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    public class SettingsSlider : MonoBehaviour
    {
        [SerializeField] private float _defaultValue;

        private Slider _slider;
        
        private void Start()
        {
            _slider = GetComponent<Slider>();
            _slider.value = _defaultValue;
        }
    }
}
