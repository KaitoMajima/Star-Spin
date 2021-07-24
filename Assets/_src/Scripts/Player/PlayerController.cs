using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.InputSystem;

namespace KaitoMajima
{
    public class PlayerController : MonoBehaviour
    {
        private PlayerInput playerInput;

        [SerializeField] private TransformReference playerDynamicReference;
        [SerializeField] private Transform fireDetectorTransform;
        [SerializeField] private float fireDetectorRadius;
        [SerializeField] private LayerMask towerMask;

        [SerializeField] private MMFeedbacks fireA_feedback;
        [SerializeField] private MMFeedbacks fireB_feedback;        

        private InputAction fireA_Action;
        private InputAction fireB_Action;

        public const int FIREA_VALUE = 0;
        public const int FIREB_VALUE = 1;

        private void Start()
        {
            playerInput = playerDynamicReference.Value.GetComponent<PlayerInput>();

            var playerActionMap = playerInput.actions.FindActionMap("Player");

            fireA_Action = playerActionMap.FindAction("FireA");

            fireA_Action.performed += OnPressedFireA;

            fireB_Action = playerActionMap.FindAction("FireB");

            fireB_Action.performed += OnPressedFireB;

        }

        private void OnPressedFireA(InputAction.CallbackContext obj)
        {
            fireA_feedback?.PlayFeedbacks();
            DetectFire(FIREA_VALUE);
        }

        private void OnPressedFireB(InputAction.CallbackContext obj)
        {
            fireB_feedback?.PlayFeedbacks();
            DetectFire(FIREB_VALUE);
        }

        private void DetectFire(int mode)
        {
            var hitCollider = Physics2D.OverlapCircle(fireDetectorTransform.position, fireDetectorRadius, towerMask);
            if(hitCollider == null)
                return;

            var towerBrain = hitCollider.GetComponent<TowerBrain>();
            if(towerBrain == null)
                return;
            
            towerBrain.HitCurrentRing(mode);
        }
        private void OnDestroy()
        {
            fireA_Action.performed -= OnPressedFireA;
            fireB_Action.performed -= OnPressedFireB;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(fireDetectorTransform.position, fireDetectorRadius);
        }
    }
}
