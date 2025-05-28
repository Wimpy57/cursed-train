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
        [SerializeField] private FullScreenPassRendererFeature _fullScreenPassRendererFeature;
        [SerializeField] private SceneToShader[] _sceneToShader;

        [Serializable]
        private struct SceneToShader
        {
            [Header("Scene transition")]
            public SceneName SceneToLoad;
            public State StateToLoadNextScene;
            public bool LoadOnStateInstantly;
            public float TimeToFade;
            [Header("Shaders")]
            public Material ShaderMaterial;
        }
        
        public static SceneChanger Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        
        private void Start()
        {
            StateManager.Instance.OnStateChanged += StateManager_OnStateChanged;
        }

        private void StateManager_OnStateChanged(object sender, EventArgs e)
        {
            if (TryFindItemByState(out SceneToShader item))
            {
                if (!item.LoadOnStateInstantly) return;
                
                StateManager.Instance.SavePlayerInfo();
                StartCoroutine(SwitchToScene(item));
            }
        }
        
        private IEnumerator SwitchToScene(SceneToShader sceneToShader)
        {
            //todo visual
            yield return StartCoroutine(Player.Instance.Fade(sceneToShader.TimeToFade));
            LoadScene(sceneToShader);
            yield return null;
        }

        private bool TryFindItemByState(out SceneToShader sceneToShader)
        {
            foreach (var item in _sceneToShader)
            {
                if (item.StateToLoadNextScene == StateManager.Instance.CurrentState)
                {
                    sceneToShader = item;
                    return true;
                }
            }
            sceneToShader = new SceneToShader();
            return false;
        }

        public void LoadScene()
        {
            foreach (var item in _sceneToShader)
            {
                if (item.StateToLoadNextScene == StateManager.Instance.CurrentState)
                {
                    StateManager.Instance.SavePlayerInfo();
                    
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
