using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using UnityEngine;

namespace KaitoMajima
{
    public class ParticleSimulationOnPayload : MonoBehaviour
    {
        [EventID] public string eventID;
        [SerializeField] private ParticleSystem currentParticleSystem;
        private void Start()
        {
            Koreographer.Instance.RegisterForEvents(eventID, TriggerActivation);
        }

        private void TriggerActivation(KoreographyEvent koreoEvent)
        {
            float simulationValue = koreoEvent.GetFloatValue();

            var main = currentParticleSystem.main;

            main.simulationSpeed = simulationValue;

        }
    }
}
