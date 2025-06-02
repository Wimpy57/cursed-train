using System.Collections;
using UnityEngine;

namespace Core.Scripts
{
    public class FadeEffect : MonoBehaviour
    {
        [SerializeField] private float _fadeDelay = 0.07f;
        [SerializeField] private float _unFadeOnSceneLoad;
    
        private Material _material;
        private bool _isFadingOut = false;

        private void Start()
        {
            _material = GetComponent<MeshRenderer>().material;
            StartCoroutine(UnFade(_unFadeOnSceneLoad));
        }

        public IEnumerator Fade(bool fadeOut, float fadeDelay = 0.07f)
        {
            Debug.Log("fadeout: " + fadeOut + " fadedelay: " + fadeDelay + " IsFadeAvailable: " + IsFadeAvailable(fadeOut));
            if (!IsFadeAvailable(fadeOut)) yield break;
            
            _isFadingOut = fadeOut;
            StopAllCoroutines();
            yield return StartCoroutine(PlayEffect(fadeOut, fadeDelay));
        }

        private IEnumerator UnFade(float fadeDelay)
        {
            _material.SetFloat("_Alpha", 1f);
            _isFadingOut = false;
            StopAllCoroutines();
            yield return StartCoroutine(PlayEffect(_isFadingOut, fadeDelay));
        }

        private bool IsFadeAvailable(bool fadeOut)
        {
            if (fadeOut && _isFadingOut)
            {
                return false;
            }

            if (!fadeOut && !_isFadingOut)
            {
                return false;
            }

            return true;
        }
        
        private IEnumerator PlayEffect(bool fadeOut, float fadeDelay)
        {
            float startAlpha = _material.GetFloat("_Alpha");
            float endAlpha = fadeOut ? 1f : 0f;
            float remainingTime = fadeDelay * Mathf.Abs(endAlpha - startAlpha);
        
            float elapsedTime = 0f;
            while (elapsedTime < fadeDelay)
            {
                elapsedTime += Time.deltaTime;
                float currentValue = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / remainingTime);
                _material.SetFloat("_Alpha", currentValue);
                yield return null;
            }
            _material.SetFloat("_Alpha", endAlpha);
        }
    }
}
