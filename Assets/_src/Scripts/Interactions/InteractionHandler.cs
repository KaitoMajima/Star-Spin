using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KaitoMajima
{
    
    public class InteractionHandler : MonoBehaviour
    {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Player player;
        [SerializeField] private Transform actorTransform;
        [SerializeField] private float detectionRadius;
        [SerializeField] private LayerMask interactableMask;
        [SerializeField] private bool debugActivated;

        [SerializeField] [ReadOnly] private Collider2D[] targets = new Collider2D[5];

        private Action onInteractableAreaCleared;
        private Action onInteractableAreaEntered;
        private Action onInteractableAreaExited;


        private TriggeredInteraction selectedInteractable;
        private int targetsCount;
        private InputAction movementAction;

        private void Start()
        {
            var playerActionMap = playerInput.actions.FindActionMap("Player");
            playerActionMap.Enable();

            movementAction = playerActionMap.FindAction("Movement");
            movementAction.performed += Interact;

            onInteractableAreaCleared += AreaClear;
            onInteractableAreaEntered += AreaEnter;
            onInteractableAreaExited += AreaExit;

        }

        private void Interact(InputAction.CallbackContext ctx)
        {
            if (selectedInteractable == null)
                return;
            selectedInteractable.OnInteract();

        }
        private void Update()
        {
            int previousTargetsCount = targetsCount;
            targetsCount = Physics2D.OverlapCircleNonAlloc(actorTransform.position, detectionRadius, targets, interactableMask);

            if(targetsCount == 0 && previousTargetsCount > 0)
                onInteractableAreaCleared?.Invoke();
            else if(targetsCount < previousTargetsCount)
                onInteractableAreaExited?.Invoke();
            else if(targetsCount > previousTargetsCount)
                onInteractableAreaEntered?.Invoke();      
        }
        
        private void AreaEnter()
        {
            Debug.Log("Enter");

            if (selectedInteractable != null)
                selectedInteractable.OnAreaExit();


            var selectedTarget = targets[0];
            
            if(!selectedTarget.TryGetComponent(out TriggeredInteraction interactable))
                return;

            selectedInteractable = interactable;
            Debug.Log(selectedInteractable.transform.parent.name);
            selectedInteractable.OnAreaEnter(); 
        }

        private void AreaExit()
        {
            Debug.Log("Exit");
            
        }
        private void AreaClear()
        {
            Debug.Log("Clear");

            targets = new Collider2D[5];
            if (selectedInteractable == null)
                return;
            selectedInteractable.OnAreaExit();
            selectedInteractable = null;
            
        }

        private void OnDrawGizmosSelected()
        {
            if (!debugActivated)
                return;

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(actorTransform.position, detectionRadius);
        }

        private void OnDestroy()
        {
            movementAction.performed -= Interact;

            onInteractableAreaCleared -= AreaClear;
            onInteractableAreaEntered -= AreaEnter;
            onInteractableAreaExited -= AreaExit;

        }
    }

}