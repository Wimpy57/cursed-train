using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;

namespace Core.Scripts
{
    public class TerrainMovement : MonoBehaviour
    {
        [SerializeField] private GameObject TerrainModel;
    
        [SerializeField] private Transform SpawnLocation;
        [SerializeField] private Transform OffsetSpawnLocation;
        

        [SerializeField] private float Velocity;
        
        private float _timer = 0f;
        private float _TimeToTravel;
        private GameObject _firstTerrainModel;
        private GameObject _secondTerrainModel;
        private float _width;
        private bool _firstTime = true;

        


        void Start()
        {
            _width = 1000;
            _TimeToTravel = _width / Velocity;
            
            
        }
  

        // Update is called once per frame
        void Update()
        {
            if (_timer <= 0f)
            {
                if (_firstTime)
                {
                    _firstTerrainModel = Instantiate(TerrainModel, OffsetSpawnLocation);
                    _firstTerrainModel.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, Velocity);
                    _secondTerrainModel = Instantiate(TerrainModel, SpawnLocation);
                    _secondTerrainModel.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, Velocity);
                    
                    _firstTime = false;
                }
                else
                {
                    _secondTerrainModel = _firstTerrainModel;
                    _firstTerrainModel = Instantiate(TerrainModel, OffsetSpawnLocation);
                    _firstTerrainModel.GetComponent<Rigidbody>().linearVelocity = new Vector3(0, 0, Velocity);
                }
                _timer = _TimeToTravel;
            }

            if ((_TimeToTravel - _timer) * Velocity >= 360f)
            {
                Destroy(_secondTerrainModel);
            }

            _timer -= Time.deltaTime;
        }
    }
}
