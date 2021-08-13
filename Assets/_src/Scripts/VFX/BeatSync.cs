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
        public static void InitializeSamples(ref BeatSyncState beatSyncState)
        {
            beatSyncState.ClipSampleData = new float[beatSyncState.sampleDataLength];
        }


        public static float GetLoudness(ref BeatSyncState beatSyncState, float[] passingSampleData)
        {
            beatSyncState.ClipSampleData = passingSampleData;

            beatSyncState.ClipLoudness = 0;
            foreach (var sample in beatSyncState.ClipSampleData)
            {
                beatSyncState.ClipLoudness += Mathf.Abs(sample);
            }
            beatSyncState.ClipLoudness /= beatSyncState.sampleDataLength;

            beatSyncState.ClipLoudness *= beatSyncState.valueFactor;
            beatSyncState.ClipLoudness = RangeConverter.ConvertValues(beatSyncState.ClipLoudness, 0, beatSyncState.limitLoudness, beatSyncState.minValue, beatSyncState.maxValue);

            return beatSyncState.ClipLoudness;
        }
    }
}

