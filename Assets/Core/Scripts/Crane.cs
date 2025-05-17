using System.Collections;
using Core.Scripts.Scenes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Core.Scripts
{
    public class Crane : AvailableAtState
    {
        [SerializeField] private SceneName _sceneToLoad;

        protected override void Start()
        {
            base.Start();
            GetComponent<XRGrabInteractable>().selectEntered.AddListener(Interact);
        }
        
        private void Interact(SelectEnterEventArgs args)
        {
            StartCoroutine(LoadScene());
            GetComponent<XRGrabInteractable>().selectEntered.RemoveListener(Interact);
        }

        private IEnumerator LoadScene()
        {
            //todo visual scene transition
            SceneManager.LoadScene(SceneInfo.SceneStringNameDictionary[_sceneToLoad]);
            yield return null;
        }
    }
}
