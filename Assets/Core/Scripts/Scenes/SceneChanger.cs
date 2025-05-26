using System;
using System.Collections;
using Core.Scripts.States;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scripts.Scenes
{
    public class SceneChanger : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private float _timeToFade;
        [SerializeField] private FullScreenPassRendererFeature _fullScreenPassRendererFeature;
        [SerializeField] private SceneToShader[] _sceneToShader;

        [Serializable]
        private struct SceneToShader
        {
            [Header("Scene transition")]
            [SerializeField] public SceneName SceneToLoad;
            [SerializeField] public State StateToLoadNextScene;
            [SerializeField] public bool LoadOnStateInstantly;
            [Header("Shaders")]
            [SerializeField] public Material ShaderMaterial;
        }
        
        public static SceneChanger Instance { get; private set; }

        private void Awake()
        {
            if (Instance is null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        private void Start()
        {
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
        }

        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            foreach (var item in _sceneToShader)
            {
                if (item.StateToLoadNextScene == StateManager.Instance.CurrentState && item.LoadOnStateInstantly)
                {
                    StartCoroutine(SwitchToScene(item));
                    break;
                }
            }
        }
        
        private IEnumerator SwitchToScene(SceneToShader sceneToShader)
        {
            //todo visual
            yield return StartCoroutine(Player.Instance.Fade(_timeToFade));
            LoadScene(sceneToShader);
            yield return null;
        }

        public void LoadScene()
        {
            foreach (var item in _sceneToShader)
            {
                if (item.StateToLoadNextScene == StateManager.Instance.CurrentState)
                {
                    StartCoroutine(SwitchToScene(item));
                    break;
                }
            }
        }
        
        private void LoadScene(SceneToShader sceneToShader)
        {
            LoadScene(sceneToShader.SceneToLoad, sceneToShader.ShaderMaterial);
        }

        private void LoadScene(SceneName sceneName, Material shaderMaterial)
        {
            _fullScreenPassRendererFeature.passMaterial = shaderMaterial;
            SceneManager.LoadScene(SceneInfo.SceneStringNameDictionary[sceneName]);
        }
        
        private void OnDisable()
        {
            StateManager.Instance.OnStateChanged -= StateManager_OnStateChanged;
        }
    }
}
