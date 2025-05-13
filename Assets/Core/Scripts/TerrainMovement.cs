using UnityEngine;

public class TerrainMovement : MonoBehaviour
{
    [SerializeField] private GameObject TerrainModel;

    [SerializeField] private Transform SpawnLocation1;
    [SerializeField] private Transform SpawnLocation2;

    [SerializeField] private float Velocity;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private float _timer = 0f;
    private GameObject _currentTerrainModel1;
    private GameObject _currentTerrainModel2;
    
    

    // Update is called once per frame
    void Update()
    {
        if (_timer <= 0f)
        {
            Destroy(_currentTerrainModel1);
            Destroy(_currentTerrainModel2);
            _currentTerrainModel1 = Instantiate(TerrainModel, SpawnLocation1);
            _currentTerrainModel2 = Instantiate(TerrainModel, SpawnLocation2);
            _currentTerrainModel1.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, 0.278f*Velocity);
            _currentTerrainModel2.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, 0.278f*Velocity);
            _timer = 216f/Velocity;
        }

        _timer -= Time.deltaTime;
    }
}
