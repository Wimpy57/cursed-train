using System.Collections;
using Core.Scripts.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scripts
{
    public class Crane : AvailableAtState
    {
        [SerializeField] private SceneName _sceneToLoad;

        private void Interact()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            //todo visual scene transition
            SceneManager.LoadScene(SceneInfo.SceneStringNameDictionary[_sceneToLoad]);
            yield return null;
        }
    }
}
