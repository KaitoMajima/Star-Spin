using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

namespace KaitoMajima
{
    
    public class SkippableHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        private InputMaster controls;
        
        public Action OnAnimationSkipped;
        private InputAction advanceAction;

        private void Start()
        {
            var uiActionMap = playerInput.actions.FindActionMap("UIExtra");
            uiActionMap.Enable();

            advanceAction = uiActionMap.FindAction("Advance");
            advanceAction.performed += SkipAnimation;

        }

        private void SkipAnimation(InputAction.CallbackContext context)
        {
            OnAnimationSkipped?.Invoke();
        }
        private void OnDestroy()
        {
            advanceAction.performed -= SkipAnimation;
        }

    }

}