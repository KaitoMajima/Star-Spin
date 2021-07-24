﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.Playables;
using UnityEngine.Events;

namespace KaitoMajima
{
    
    public class InteractableSimple : SerializedMonoBehaviour, IInteractable
    {
        [SerializeField] private TriggeredInteraction interaction;
        [SerializeField] private TweenController[] animationObjs;
        [SerializeField] private PlayableDirector timeline;

        private bool canInteract = true;
        public UnityEvent onAreaInteract;
        public UnityEvent onAreaEnter;
        public UnityEvent onAreaExit;

        private void Start()
        {
            interaction.Interact += OnInteract;
            interaction.AreaEntered += OnAreaEnter;
            interaction.AreaExited += OnAreaExit;
        }

        private void OnAreaEnter()
        {
            foreach(var obj in animationObjs)
            {
                obj.Activate();
            }

            onAreaEnter?.Invoke();
        }
        private void OnAreaExit()
        {
            foreach (var obj in animationObjs)
            {
                obj.Deactivate();
            }
            onAreaExit?.Invoke();
        }
        private void OnInteract()
        {
            if(!canInteract)
                return;
            onAreaInteract?.Invoke();
            
            if(timeline == null)
                return;
            
            timeline.time = 0;
            timeline.Play();

        }

        #region Signal Receiver Methods

        public void DisableFurtherInteractions()
        {
            canInteract = false;
        }

        public void EnableFurtherInteractions()
        {
            canInteract = true;
        }
        #endregion
        private void OnDestroy()
        {
            interaction.Interact -= OnInteract;
            interaction.AreaEntered -= OnAreaEnter;
            interaction.AreaExited -= OnAreaExit;
        }
    }

}