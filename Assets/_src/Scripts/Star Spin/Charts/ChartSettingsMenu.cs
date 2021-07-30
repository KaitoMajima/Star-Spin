using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KaitoMajima
{
    public class ChartSettingsMenu : MonoBehaviour
    {
        [SerializeField] private LevelSelectCharts levelSelectCharts;

        private Chart chart;

        [Header("Contents")]
        [SerializeField] private ChartChanger[] chartChangers;
        [SerializeField] private TextMeshProUGUI musicNameTextComponent;

        [SerializeField] private TextMeshProUGUI mapNameTextComponent;

        [SerializeField] private Image mapCoverImage;
        [SerializeField] private SceneLoader sceneLoader;
        private void OnEnable()
        {
            chart = levelSelectCharts.CurrentChart;
            InitializeContents();
        }
        private void InitializeContents()
        {
            musicNameTextComponent.text = chart.musicName;
            mapNameTextComponent.text = "Map: " + chart.mapName;
            sceneLoader.sceneName = chart.sceneName;

            mapCoverImage.sprite = chart.mapCover;
            mapCoverImage.SetNativeSize();

            foreach (var chartChanger in chartChangers)
            {
                chartChanger.ringTimingOptions = chart.ringTimingOptions;
                chartChanger.Initialize();
            }
            
        }
    }
}
