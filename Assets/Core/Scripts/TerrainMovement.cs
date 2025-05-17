using UnityEngine;

public class TerrainMovement : MonoBehaviour
{
    [SerializeField] private GameObject TerrainModel;
    
    [SerializeField] private Transform SpawnLocation;

    [SerializeField] private float Velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float _timer = 0f;
    private GameObject _currentTerrainModel;

  

    // Update is called once per frame
    void Update()
    {
        if (_timer <= 0f)
        {            
            Destroy(_currentTerrainModel);         
            _currentTerrainModel = Instantiate(TerrainModel, SpawnLocation);
            _currentTerrainModel.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, 0.278f * Velocity);
            _timer = 216f/Velocity;
        }

        _timer -= Time.deltaTime;
    }
}
