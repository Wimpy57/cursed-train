using UnityEngine;

public class GameStartShaderSetup : MonoBehaviour
{
    [SerializeField] FullScreenPassRendererFeature _fullScreenPassRendererFeature;
    [SerializeField] Material _shaderMaterial;
    void Start()
    {
        _fullScreenPassRendererFeature.passMaterial = _shaderMaterial;
    }

    
}
