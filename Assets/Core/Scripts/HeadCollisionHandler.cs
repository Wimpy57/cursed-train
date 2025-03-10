using System.Collections.Generic;
using UnityEngine;

public class HeadCollisionHandler : MonoBehaviour
{
    [SerializeField] private HeadCollisionDetector _detector;
    [SerializeField] private CharacterController _characterController;
    [SerializeField] private float _pushBackStrength = 1f;

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
            return;
        }
        Vector3 pushBackDirection = CalculatePushBackDirection(_detector.DetectedColliderHits);
        _characterController.Move(pushBackDirection.normalized * (_pushBackStrength * Time.deltaTime));
    }
}
