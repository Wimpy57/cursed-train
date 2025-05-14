using System.Linq;
using Core.Scripts.Achievements;
using Core.Scripts.States;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

namespace Core.Scripts.Doors
{
    public class ConductorDoor : Door
    {
        protected override void Start()
        {
            base.Start();
            foreach (var handle in Handles)
            {
                handle.GetComponent<XRGrabInteractable>().selectEntered.AddListener(SelectHandle);
            }
        }
        
        private void SelectHandle(SelectEnterEventArgs e)
        {
            if (!AvailableAtStates.Contains(StateManager.Instance.CurrentState))
            {
                AchievementManager.Instance.GenerateAchievement(Achievement.HoldYourHorses);
                e.interactableObject.selectEntered.RemoveListener(SelectHandle);
            }
        }
    }
}
