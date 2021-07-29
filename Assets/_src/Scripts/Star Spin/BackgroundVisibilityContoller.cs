using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KaitoMajima
{
    public class BackgroundVisibilityContoller : MonoBehaviour
    {
        [SerializeField] private RingTimingOptions ringTimingOptions;
        [SerializeField] private Image background;
        void Start()
        {
            var bgColor = background.color;

            var newColor = new Color(bgColor.r, bgColor.g, bgColor.b, ringTimingOptions.BackgroundVisibility);

            background.color = newColor;

        }

    }
}
