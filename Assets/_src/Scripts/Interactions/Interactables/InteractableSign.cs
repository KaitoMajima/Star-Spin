using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Playables;

namespace KaitoMajima
{
    
    public class InteractableSign : SerializedMonoBehaviour, IInteractable
    {
        [SerializeField] private TriggeredInteraction interaction;
        [SerializeField] private TweenController[] animationObjs;
        [SerializeField] private GameObject alertObj;
        [SerializeField] private PlayableDirector timeline;

        private void Start()
        {
            interaction.Interact += OpenMenu;
            interaction.AreaEntered += ShowIndicationArrow;
            interaction.AreaExited += HideIndicationArrow;
        }

        private void ShowIndicationArrow()
        {
            foreach(var obj in animationObjs)
            {
                obj.Activate();
            }
        }
        private void HideIndicationArrow()
        {
            foreach (var obj in animationObjs)
            {
                obj.Deactivate();
            }
        }
        private void OpenMenu()
        {
            if(alertObj != null)
                alertObj.SetActive(false);
            
            if(timeline == null)
                return;
            timeline.time = 0;
            timeline.Play();
        }

        private void OnDestroy()
        {
            interaction.Interact -= OpenMenu;
            interaction.AreaEntered -= ShowIndicationArrow;
            interaction.AreaExited -= HideIndicationArrow;
        }
    }

}