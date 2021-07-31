using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;

namespace KaitoMajima
{
    public class WinOnPayload : MonoBehaviour
    {
        [EventID] public string eventID;
        private void Start()
        {
            Koreographer.Instance.RegisterForEvents(eventID, TriggerActivation);
        }

        private void TriggerActivation(KoreographyEvent koreoEvent)
        {   
            GameManager.OnGameWon?.Invoke();
        }
    }
}
