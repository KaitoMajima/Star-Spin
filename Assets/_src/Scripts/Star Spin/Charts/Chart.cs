using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    [CreateAssetMenu(fileName = "Chart", menuName = "Star Spin/Options/Chart")]
    public class Chart : ScriptableObject
    {
        public string musicName;
        public string difficulty;
        public RingTimingOptions ringTimingOptions;
        public Sprite cycleSprite;
        public Sprite musicCover;
        public string mapName;
        public Sprite mapCover;
        public AudioClip musicClip;
        public float musicPreviewTime;
        public string sceneName;

    }
}
