using System.Collections.Generic;
using UnityEngine;

namespace Core.Scripts
{
    public class HeadCollisionHandler : MonoBehaviour
    {
        [SerializeField] private HeadCollisionDetector _detector;
        [SerializeField] private CharacterController _characterController;
        [SerializeField] private float _pushBackStrength = 1f;
        [SerializeField] private HeadCollisionTypeAction _typeAction;
        [SerializeField] private FadeEffect _fadeEffect;

        private Vector3 CalculatePushBackDirection(List<RaycastHit> colliderHits)
        {
            Vector3 combinedNormal = Vector3.zero;
            foreach (var hitPoint in colliderHits)
            {
                combinedNormal += new Vector3(hitPoint.normal.x, 0f, hitPoint.normal.z);
            }
            return combinedNormal;
        }

        private void Update()
        {
            if (_detector.DetectedColliderHits.Count <= 0)
            {
                if (_typeAction != HeadCollisionTypeAction.PushBack)
                {
                    _fadeEffect.Fade(false);
                }
                return;
            }

            if (_detector.IsInsideCollider && _typeAction != HeadCollisionTypeAction.PushBack)
            {
                _fadeEffect.Fade(true);
            }

            switch (_typeAction)
            {
                case HeadCollisionTypeAction.PushBack :
                    PushBack();
                    break;
                case HeadCollisionTypeAction.BlackScreen :
                    BlackScreen();
                    break;
                case HeadCollisionTypeAction.BlackScreenAndPushBack :
                    BlackScreen();
                    PushBack();
                    break;
            }
        }

        private void PushBack()
        {
            Vector3 pushBackDirection = CalculatePushBackDirection(_detector.DetectedColliderHits);
            _characterController.Move(pushBackDirection.normalized * (_pushBackStrength * Time.deltaTime));
        }

        private void BlackScreen()
        {
            _fadeEffect.Fade(true);
        }
    }

    public enum HeadCollisionTypeAction
    {
        BlackScreen,
        PushBack,
        BlackScreenAndPushBack,
    }
}