using System.Collections;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace Core.Scripts
{
    public class BlinkingLamp : MonoBehaviour
    {
        [Header("Parameters")] 
        [SerializeField] private bool _isInverted;
        [SerializeField] private float _lowerIntensityPercent;
        [SerializeField] private float _maxBlinkingDelay;
        [SerializeField] private float _minBlinkingDelay;
        [SerializeField] private float _maxBlinkingFrequency;
        [SerializeField] private float _minBlinkingFrequency;
        [Header("Light")] 
        [SerializeField] private Light _light;
        [SerializeField] private float _invertedRange;
        
        private float _higherIntensityValue;
        private float _currentBlinkingDelay;
        private float _timer;
        
        private void Start()
        {
            _higherIntensityValue = _light.intensity;
            if (_isInverted)
            {
                _light.range = _invertedRange;
                _light.intensity = 0f;
            }
            _currentBlinkingDelay = Random.Range(_minBlinkingDelay, _maxBlinkingDelay);
        }

        private void Update()
        {
            if (_timer >= _currentBlinkingDelay)
            {
                _currentBlinkingDelay = Random.Range(_minBlinkingDelay, _maxBlinkingDelay);
                _timer = 0f;
                StartCoroutine(Blink());
            }
            _timer += Time.deltaTime;
        }

        private IEnumerator Blink()
        {
            float blinkingFrequency = Random.Range(_minBlinkingFrequency, _maxBlinkingFrequency);
            for (int i = 0; i < Random.Range(1, 3); i++)
            {
                float intensity = _isInverted ? 
                    Random.Range(0f, _lowerIntensityPercent / 100) : Random.Range(_lowerIntensityPercent/100, 1);
                
                yield return StartCoroutine(ChangeIntensity(_light.intensity, intensity * _higherIntensityValue, blinkingFrequency));
            }
            float finalIntensity = _isInverted ? 0f : _higherIntensityValue;
            yield return StartCoroutine(ChangeIntensity(_light.intensity, finalIntensity, blinkingFrequency));

        }

        private IEnumerator ChangeIntensity(float startIntensity, float endIntensity, float duration)
        {
            for (int i = 0; i <= duration * 100+1; i++)
            {
                _light.intensity = Vector2.Lerp(new Vector2(0, startIntensity), new Vector2(0, endIntensity), (i/(duration*100))).Y;
                yield return new WaitForSeconds(.001f);
            }
        }
    }
}
