using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace KaitoMajima
{
    public class RingScale : TweenController
    {
        [SerializeField] 
        private Transform scaledTransform;

        [SerializeField]
        private PlayOnEnableTween playOnEnable = PlayOnEnableTween.None;

        [SerializeField]
        private RingTimingOptions ringTimingOptions;

        [SerializeField]
        private TweenSettings tweenSettings = TweenSettings.Default;

        [SerializeField]
        private TweenScaleValueSettings tweenScaleValueSettings = TweenScaleValueSettings.Default;

        [SerializeField] private bool destroyOnEndDeactivate;

        private Vector3 originalScale;
        
        private void Awake()
        {
            tweenSettings.duration = ringTimingOptions.NoteLength;
        }
        private void Start()
        {
            Vector3 firstOriginalScale = scaledTransform.localScale;
            originalScale = scaledTransform.localScale;
            // originalScale = firstOriginalScale * ringTimingOptions.NoteLength / ringTimingOptions.noteDisappearTime;
            // scaledTransform.localScale = originalScale;
        }
        private void OnEnable()
        {
            
            if(playOnEnable == PlayOnEnableTween.Activate)
                Activate();
            if(playOnEnable == PlayOnEnableTween.Deactivate)
                Deactivate();
        }
        public override void Activate()
        {

            if(!tweenSettings.useLoops)
            {
                if(tweenScaleValueSettings.resetValues)
                    scaledTransform.localScale = originalScale;
                    
                if(tweenScaleValueSettings.scaleType == TweenScaleValueSettings.ScaleType.FixedVectorScale)
                    scaledTransform.DOScale(tweenScaleValueSettings.fixedScale, tweenSettings.duration).SetEase(tweenSettings.easeType);
                else
                    scaledTransform.DOScale(tweenScaleValueSettings.rateScale, tweenSettings.duration).SetEase(tweenSettings.easeType);
                
                
            }
            else
            {
                if(tweenScaleValueSettings.scaleType == TweenScaleValueSettings.ScaleType.FixedVectorScale)
                    scaledTransform.DOScale(tweenScaleValueSettings.fixedScale, tweenSettings.duration).SetEase(tweenSettings.easeType).SetLoops(-1, tweenSettings.loopType);
                else
                    scaledTransform.DOScale(tweenScaleValueSettings.rateScale, tweenSettings.duration).SetEase(tweenSettings.easeType).SetLoops(-1, tweenSettings.loopType);
            }
            
        }

        public override void Deactivate()
        {
            scaledTransform.DOKill();
            scaledTransform.DOScale(originalScale, tweenSettings.duration).SetEase(tweenSettings.easeType).OnComplete(DestroyOnEndDeactivate);


        }

        private void DestroyOnEndDeactivate()
        {
            Destroy(gameObject);
        }
    }
}
