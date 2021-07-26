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
            Square = 1,
            Star = 2
        }
        public RingMode ringMode = RingMode.Circle;

        [SerializeField] private RingTimingOptions timingOptions;

        [Header("Ring Hit Feedbacks")]
        [SerializeField] private MMFeedbacks earlyHitFeedback;
        [SerializeField] private MMFeedbacks perfectHitFeedback;
        [SerializeField] private MMFeedbacks lateHitFeedback;
        [SerializeField] private MMFeedbacks misfireHitFeedback;
        [SerializeField] private MMFeedbacks missHitFeedback;

        [Header("Ring Mode Hit Feedbacks")]
        [SerializeField] private MMFeedbacks hitFeedback;

        private bool hasBeenHit;

        private bool starFireAPressed;
        private bool starFireBPressed;

        private float doubleClickTime = 0.25f;
        private float doubleClickTimer;

        private Coroutine doubleClickCoroutine;

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
                if(ringState == RingState.SuperEarly && timer >= timingOptions.earlyGoodWindow)
                    ringState = RingState.EarlyGood;
                if(ringState == RingState.EarlyGood && timer >= timingOptions.perfectWindow)
                    ringState = RingState.Perfect;
                if(ringState == RingState.Perfect && timer >= timingOptions.lateWindow)
                    ringState = RingState.LateGood;
                if(ringState == RingState.LateGood && timer >= timingOptions.missWindow)
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
                    SucessfulHitRing();
                    break;
                case RingState.Perfect:
                    perfectHitFeedback?.PlayFeedbacks();
                    SucessfulHitRing();
                    break;
                case RingState.LateGood:
                    lateHitFeedback?.PlayFeedbacks();
                    SucessfulHitRing();
                    break;
                case RingState.SuperEarly:
                    missHitFeedback?.PlayFeedbacks();
                    break;
            }
            RemoveRing();
        }

        public void StarHit(int fireMode)
        {
            if(fireMode == (int)RingMode.Circle)
                starFireAPressed = true;
            if(fireMode == (int)RingMode.Square)
                starFireBPressed = true;
            
            if(starFireAPressed && starFireBPressed)
                HitRing();
            
            if(doubleClickTimer > 0)
                return;

            doubleClickCoroutine = StartCoroutine(DoubleClickTimer());
        }

        private IEnumerator DoubleClickTimer()
        {
            doubleClickTimer = doubleClickTime;
            while (doubleClickTimer > 0)
            {
                doubleClickTimer -= Time.deltaTime;
                yield return null;
            } 
            MisfireRing();
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
            if(doubleClickCoroutine != null)
                StopCoroutine(doubleClickCoroutine);
            StartCoroutine(SkipFrame(() => Destroy(gameObject)));
        }

        private void SucessfulHitRing()
        {
            hitFeedback?.PlayFeedbacks();
        }
        private IEnumerator SkipFrame(Action callback)
        {
            yield return null;
            callback?.Invoke();
        }
    }
}
