using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "RingTimingOptions", menuName = "Defend The Beat/Options/Ring Timing")]
    public class RingTimingOptions : ScriptableObject
    {
        [SerializeField] private float earlyGoodWindow = 1.2f;
        [SerializeField] private float perfectWindow = 1.3f;
        [SerializeField] private float lateWindow = 1.5f;
        [SerializeField] private float missWindow = 1.6f;
        public float noteDisappearTime = 1.8f;

        [Range(0, 6)]
        public int noteLengthLevel = 0;

        public float EarlyGoodWindow
        {
            get
            {
                return earlyGoodWindow * NoteLength / noteDisappearTime;
            }
        }
        public float PerfectWindow
        {
            get
            {
                return perfectWindow * NoteLength / noteDisappearTime;
            }
        }
        public float LateWindow
        {
            get
            {
                return lateWindow * NoteLength / noteDisappearTime;
            }
        }
        public float MissWindow
        {
            get
            {
                return missWindow * NoteLength / noteDisappearTime;
            }
        }
        public float NoteLength 
        {
            get
            {
                return noteDisappearTime - NoteSpread;
            }
            
        }

        public float NoteSpread
        {
            get
            {
                return 0.2f * (6 - noteLengthLevel);
            }
        }

    }
}
