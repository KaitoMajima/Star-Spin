using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    public class VisualBeatSync : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;

        [SerializeField] private TransformReference musicDynamicReference;

        [SerializeField] private float updateStep = 0.1f;

        [SerializeField] private BeatSyncState beatSyncState = BeatSyncState.Default;

        private float currentUpdateTime = 0;

        private float[] passingSampleData;
        [SerializeField] private Transform targetTransform; 

        private void Start()
        {
            if(musicDynamicReference != null)
                musicSource = musicDynamicReference.Value.GetComponent<AudioSource>();
            
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
                
                var loudness = BeatSync.GetLoudness(ref beatSyncState, passingSampleData);
                targetTransform.localScale = new Vector3(loudness, loudness, targetTransform.localScale.z);
            }
        }
    }
}
