using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KaitoMajima
{
    public class FollowReticle : MonoBehaviour
    {
        private PlayerInput playerInput;
        [SerializeField] private TransformReference playerDynamicReference;
        
        [SerializeField] private Transform reticleTransform;

        private Camera mainCamera;
        [SerializeField] private TransformReference cameraDynamicReference;
        private Vector2 mousePosition;
        private InputAction aimAction;

        public Vector2 RawMousePosition {get; private set;}

        private void Start()
        {
            playerInput = playerDynamicReference.Value.GetComponent<PlayerInput>();
            mainCamera = cameraDynamicReference.Value.GetComponent<Camera>();

            
            var playerActionMap = playerInput.actions.FindActionMap("Player");

            aimAction = playerActionMap.FindAction("Aim");
            aimAction.performed += InputMousePosition;
        }

        private void InputMousePosition(InputAction.CallbackContext context)
        {
            RawMousePosition = context.ReadValue<Vector2>();
        }

        private void LateUpdate()
        {
            mousePosition = mainCamera.ScreenToWorldPoint(RawMousePosition);
            reticleTransform.position = mousePosition;
        }

    }
}
