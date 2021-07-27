using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    public class VisualBeatSync : MonoBehaviour
    {
        [SerializeField] private AudioSource musicSource;

        [SerializeField] private float updateStep;

        [SerializeField] private int sampleDataLength = 1024;

        private float currentUpdateTime = 0;

        [SerializeField] private float clipLoudness;

        private float[] clipSampleData;
        [SerializeField] private Transform targetTransform; 
        [SerializeField] private float sizeFactor = 1;

        [SerializeField] private float limitLoudness = 20;
        [SerializeField] private float minSize;
        [SerializeField] private float maxSize = 500;

        private void Awake()
        {
            clipSampleData = new float[sampleDataLength];
        }

        private void Update()
        {
            currentUpdateTime += Time.deltaTime;
            if(currentUpdateTime >= updateStep)
            {
                currentUpdateTime = 0;
                musicSource.clip.GetData(clipSampleData, musicSource.timeSamples);
                clipLoudness = 0;
                foreach (var sample in clipSampleData)
                {
                    clipLoudness += Mathf.Abs(sample);
                }
                clipLoudness /= sampleDataLength;

                clipLoudness *= sizeFactor;
                clipLoudness = RangeConverter.ConvertValues(clipLoudness, 0, limitLoudness, minSize, maxSize);
                targetTransform.localScale = new Vector3(clipLoudness, clipLoudness, targetTransform.localScale.z);
            }
        }
    }
}
