using UnityEngine;
using System.Collections.Generic;

public class HeadCollisionDetector : MonoBehaviour
{
    [SerializeField, Range(0f, 0.5f)] private float _detectionDelay = 0.5f;
    [SerializeField] private float _detectionDistance = 0.2f;
    [SerializeField] private LayerMask _detectionLayer;

    public List<RaycastHit> DetectedColliderHits { get; private set; }

    private float _currentTime = 0f;

    private void Start()
    {
        DetectedColliderHits = new List<RaycastHit>();
    }

    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime > _detectionDelay)
        {
            _currentTime = 0f;
            DetectedColliderHits = PerformDetection(transform.position, _detectionDistance, _detectionLayer);
        }
    }
    
    private List<RaycastHit> PerformDetection(Vector3 position, float distance, LayerMask mask)
    {
        List<RaycastHit> detectedHits = new List<RaycastHit>();
        List<Vector3> directions = new List<Vector3> { Vector3.forward, Vector3.right, Vector3.left };
        RaycastHit hit;

        foreach (var dir in directions) 
        {
            if (Physics.Raycast(position, dir, out hit, distance, mask))
            {
                detectedHits.Add(hit);
            }
        }
        return detectedHits;
    }
}
