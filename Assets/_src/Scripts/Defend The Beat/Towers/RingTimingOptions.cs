using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    using UnityEngine;
    
    [CreateAssetMenu(fileName = "RingTimingOptions", menuName = "Defend The Beat/Options/Ring Timing")]
    public class RingTimingOptions : ScriptableObject
    {
        public float earlyGoodWindow = 0.4f;
        public float perfectWindow = 0.5f;
        public float lateWindow = 0.6f;
        public float missWindow = 0.7f;
    }
}
