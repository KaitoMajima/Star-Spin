using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using SonicBloom.Koreo;
using UnityEngine;

namespace KaitoMajima
{
    public class FeedbackCyclePayload : MonoBehaviour
    {
        [EventID] public string eventID;

        [SerializeField] private bool onOffMode;
        private bool isActivated;
        private int cycleIndex;
        [SerializeField] private MMFeedbacks[] payloadFeedbacks;
        private void Start()
        {
            Koreographer.Instance.RegisterForEvents(eventID, TriggerActivation);
        }

        private void TriggerActivation(KoreographyEvent koreoEvent)
        {
            Debug.Log(cycleIndex);
            var payloadFeedback = payloadFeedbacks[cycleIndex];
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

            cycleIndex++;
            cycleIndex %= payloadFeedbacks.Length;
        }
    }
}
