using UnityEngine;

public class EarthRotation : MonoBehaviour
{

    [SerializeField] private float Speed;
    
    // Update is called once per frame
    void Update()
    {
        transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y + Time.deltaTime * Speed, transform.localEulerAngles.z);
    }
}
