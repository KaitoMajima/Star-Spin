using System;
using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;

namespace KaitoMajima
{
    public class ActivateOnPayload : MonoBehaviour
    {
        [EventID] public string eventID;
        private bool isActivated;

        [SerializeField] private GameObject[] objectsToActivate;
        private void Start()
        {
            Koreographer.Instance.RegisterForEvents(eventID, TriggerActivation);
        }

        private void TriggerActivation(KoreographyEvent koreoEvent)
        {
            isActivated = !isActivated;
            foreach (var obj in objectsToActivate)
            {
                obj.SetActive(isActivated);
            }
        }
    }
}
