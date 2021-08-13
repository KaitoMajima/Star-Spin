using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    public class VisualBeatSync : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;

        [SerializeField] private float updateStep;

        [SerializeField] private BeatSyncState beatSyncState = BeatSyncState.Default;

        private float currentUpdateTime = 0;

        private float[] passingSampleData;
        [SerializeField] private Transform targetTransform; 

        private void Awake()
        {
            passingSampleData = new float[beatSyncState.sampleDataLength];
            BeatSync.InitializeSamples(ref beatSyncState);
        }

        private void Update()
        {
            currentUpdateTime += Time.deltaTime;
            if(currentUpdateTime >= updateStep)
            {
                currentUpdateTime = 0;
                musicSource.clip.GetData(passingSampleData, musicSource.timeSamples);
                beatSyncState.ClipSampleData = passingSampleData;

                var loudness = BeatSync.GetLoudness(ref beatSyncState);
                targetTransform.localScale = new Vector3(loudness, loudness, targetTransform.localScale.z);
            }
        }
    }
}
