using System;
using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;

namespace KaitoMajima
{
    public class RingDetection : MonoBehaviour
    {
        [HideInInspector] public TowerBrain brainParent;

        public enum RingState 
        {
            SuperEarly,
            EarlyGood,

            Perfect,
            LateGood,
            Miss,
            Misfire
        }
        public RingState ringState = RingState.SuperEarly;

        public enum RingMode
        {
            Circle = 0,
            Square = 1
        }
        public RingMode ringMode = RingMode.Circle;
        [SerializeField] private float earlyGoodWindow = 0.15f;
        [SerializeField] private float perfectWindow = 0.2f;
        [SerializeField] private float lateWindow = 0.4f;
        [SerializeField] private float missWindow = 0.6f;

        [Header("Ring Hit Feedbacks")]
        [SerializeField] private MMFeedbacks earlyHitFeedback;
        [SerializeField] private MMFeedbacks perfectHitFeedback;
        [SerializeField] private MMFeedbacks lateHitFeedback;
        [SerializeField] private MMFeedbacks misfireHitFeedback;
        [SerializeField] private MMFeedbacks missHitFeedback;

        private bool hasBeenHit;
        private float timer;
        private Coroutine timerCoroutine;
        private void Start()
        {
            timerCoroutine = StartCoroutine(RingTimer());
        }

        private IEnumerator RingTimer()
        {
            timer = 0;
            while(true)
            {
                timer += Time.deltaTime;
                if(ringState == RingState.SuperEarly && timer >= earlyGoodWindow)
                    ringState = RingState.EarlyGood;
                if(ringState == RingState.EarlyGood && timer >= perfectWindow)
                    ringState = RingState.Perfect;
                if(ringState == RingState.Perfect && timer >= lateWindow)
                    ringState = RingState.LateGood;
                if(ringState == RingState.LateGood && timer >= missWindow)
                {
                    ringState = RingState.Miss;
                    MissRing();
                }
                    
                yield return null;
            }
        }
        public void HitRing()
        {
            StopCoroutine(timerCoroutine);

            switch (ringState)
            {
                case RingState.EarlyGood:
                    earlyHitFeedback?.PlayFeedbacks();
                    break;
                case RingState.Perfect:
                    perfectHitFeedback?.PlayFeedbacks();
                    break;
                case RingState.LateGood:
                    lateHitFeedback?.PlayFeedbacks();
                    break;
                case RingState.SuperEarly:
                    missHitFeedback?.PlayFeedbacks();
                    break;
            }
            RemoveRing();
        }
        public void MisfireRing()
        {
            StopCoroutine(timerCoroutine);
            ringState = RingState.Misfire;
            misfireHitFeedback?.PlayFeedbacks();
            RemoveRing();
        }

        public void MissRing()
        {
            StopCoroutine(timerCoroutine);
            ringState = RingState.Miss;
            missHitFeedback?.PlayFeedbacks();
            RemoveRing();
        }

        private void RemoveRing()
        {
            if(brainParent == null)
                return;
            brainParent.RemoveRing(this);
            StartCoroutine(SkipFrame(() => Destroy(gameObject)));
        }
        private IEnumerator SkipFrame(Action callback)
        {
            yield return null;
            callback?.Invoke();
        }
    }
}
