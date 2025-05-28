using UnityEngine;

namespace Core.Scripts
{
    public class TerrainMovement : MonoBehaviour
    {
        [SerializeField] private GameObject TerrainModel;
    
        [SerializeField] private Transform SpawnLocation;

        [SerializeField] private float Velocity;
        
        private float _timer = 0f;
        private GameObject _currentTerrainModel;

  

        // Update is called once per frame
        void Update()
        {
            if (_timer <= 0f)
            {            
                Destroy(_currentTerrainModel);         
                _currentTerrainModel = Instantiate(TerrainModel, SpawnLocation);
                _currentTerrainModel.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, Velocity);
                _timer = 180f/Velocity;
            }

            _timer -= Time.deltaTime;
        }
    }
}
