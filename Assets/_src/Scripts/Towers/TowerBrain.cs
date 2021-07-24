using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    public class TowerBrain : MonoBehaviour
    {
        [SerializeField] private List<RingDetection> rings = new List<RingDetection>();

        public void AddRing(RingDetection ring)
        {
            rings.Add(ring);
            ring.brainParent = this;
        }

        public void RemoveRing(RingDetection ring)
        {
            rings.Remove(ring);
        }

        public void HitCurrentRing(int fireMode)
        {
            if(rings.Count == 0)
                return;
            var currentRing = rings[0];
            if(currentRing == null)
                return;
            
            int ringMode = (int)currentRing.ringMode;
            if(fireMode == ringMode)
                currentRing.HitRing();
            else
                currentRing.MisfireRing();

        }
    }
}
