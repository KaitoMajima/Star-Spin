using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace KaitoMajima
{
    public class RingFade : TweenController
    {
        [SerializeField]
        private SpriteRenderer spriteRenderer;

        [SerializeField]
        private PlayOnEnableTween playOnEnable = PlayOnEnableTween.None;

        [SerializeField]
        private bool destroyOnEndDeactivate;

        [SerializeField]
        private TweenSettings tweenSettings = TweenSettings.Default;

        public RingTimingOptions ringTimingOptions;

        private void OnEnable()
        {
            if(playOnEnable == PlayOnEnableTween.Activate)
                Activate();
            if(playOnEnable == PlayOnEnableTween.Deactivate)
                Deactivate();
        }
        public override void Activate()
        {
            tweenSettings.duration = ringTimingOptions.EarlyGoodWindow;
            spriteRenderer.DOFade(1, tweenSettings.duration).SetEase(tweenSettings.easeType);
        }

        public override void Deactivate()
        {
            tweenSettings.duration = ringTimingOptions.EarlyGoodWindow;
            spriteRenderer.DOFade(0, tweenSettings.duration).SetEase(tweenSettings.easeType).OnComplete(DestroyOnEnd);
        }

        private void DestroyOnEnd()
        {
            if(destroyOnEndDeactivate)
                Destroy(gameObject);
        }
    }
}
