using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using SonicBloom.Koreo;
using UnityEngine;

namespace KaitoMajima
{
    public class FeedbackOnPayload : MonoBehaviour
    {
        [EventID] public string eventID;

        [SerializeField] private bool onOffMode;
        private bool isActivated;
        [SerializeField] private MMFeedbacks payloadFeedback;
        private void Start()
        {
            Koreographer.Instance.RegisterForEvents(eventID, TriggerActivation);
        }

        private void TriggerActivation(KoreographyEvent koreoEvent)
        {
            if(onOffMode)
            {
                isActivated = !isActivated;
                if(isActivated)
                    payloadFeedback?.PlayFeedbacks();
                else
                    payloadFeedback?.StopFeedbacks();
            }
            else
                payloadFeedback?.PlayFeedbacks();
        }
    }
}
