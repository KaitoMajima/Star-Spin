using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    [Serializable]
    public struct BeatSyncState
    {
        public int sampleDataLength;

        public float valueFactor;

        public float limitLoudness;
        public float minValue;
        public float maxValue;
        public float ClipLoudness {get => clipLoudness; set => clipLoudness = value;}
        private float clipLoudness;
        public float[] ClipSampleData {get => clipSampleData; set => clipSampleData = value;}
        private float[] clipSampleData;

        public static BeatSyncState Default = new BeatSyncState()
        {
            sampleDataLength = 1024,
            valueFactor = 10,
            limitLoudness = 20,
            minValue = 1,
            maxValue = 5
        };

    }

    public static class BeatSync
    {
        public static void InitializeSamples(ref BeatSyncState beatState)
        {
            beatState.ClipSampleData = new float[beatState.sampleDataLength];
        }


        public static float GetLoudness(ref BeatSyncState beatState)
        {

            beatState.ClipLoudness = 0;
            foreach (var sample in beatState.ClipSampleData)
            {
                beatState.ClipLoudness += Mathf.Abs(sample);
            }
            beatState.ClipLoudness /= beatState.sampleDataLength;

            beatState.ClipLoudness *= beatState.valueFactor;
            beatState.ClipLoudness = RangeConverter.ConvertValues(beatState.ClipLoudness, 0, beatState.limitLoudness, beatState.minValue, beatState.maxValue);

            return beatState.ClipLoudness;
        }
    }
}

