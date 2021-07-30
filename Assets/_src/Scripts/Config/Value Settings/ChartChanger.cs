using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KaitoMajima
{
    public class ChartChanger : MonoBehaviour
    {
        public RingTimingOptions ringTimingOptions;
        [SerializeField] private Slider slider;

        public enum ChartSettingType
        {
            BGVisibility = 0,
            NoteLength = 1
        }
        [SerializeField] private ChartSettingType chartSettingType;
        [SerializeField] private TextMeshProUGUI valueTextComponent;

        public void Initialize()
        {
            if(chartSettingType == ChartSettingType.BGVisibility)
            {
                slider.SetValueWithoutNotify(ringTimingOptions.backgroundVisibilityLevel);
                if(valueTextComponent != null) valueTextComponent.text = ringTimingOptions.backgroundVisibilityLevel.ToString();
            }
            else if(chartSettingType == ChartSettingType.NoteLength)
            {
                slider.SetValueWithoutNotify(ringTimingOptions.noteLengthLevel);
                if(valueTextComponent != null) valueTextComponent.text = ringTimingOptions.noteLengthLevel.ToString();
            }
            
            

            slider.onValueChanged.AddListener(Change);
        }
        public void Change(float value)
        {
            if(chartSettingType == ChartSettingType.BGVisibility)
                ringTimingOptions.backgroundVisibilityLevel = (int)value;
            else if(chartSettingType == ChartSettingType.NoteLength)
                ringTimingOptions.noteLengthLevel = (int)value;
        }

        private void OnDestroy()
        {
            slider.onValueChanged.RemoveListener(Change);
        }
    }
}
