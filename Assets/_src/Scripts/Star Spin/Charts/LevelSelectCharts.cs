using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KaitoMajima
{
    public class LevelSelectCharts : MonoBehaviour
    {
        [SerializeField] private List<Chart> charts;

        [Header("Contents")]

        [SerializeField] private TextMeshProUGUI musicNameTextComponent;
        [SerializeField] private TextMeshProUGUI difficultyTextComponent;

        [SerializeField] private Image cycleImage;

        [SerializeField] private Image playCycleImage;
        [SerializeField] private Image playCycleMask;

        [SerializeField] private Image musicCoverImage;

        [SerializeField] private AudioSource musicSource;
        
        private int chartIndex = 0;

        public Chart CurrentChart
        {
            get
            {
                return charts[chartIndex];
            }
        }
        private void Start()
        {
            InitializeContents();
        }
        public void ChangeForward()
        {
            chartIndex++;
            if(chartIndex >= charts.Count)
                chartIndex = 0;
            InitializeContents();
        }
        public void ChangeBackward()
        {
            chartIndex--;
            if(chartIndex < 0)
                chartIndex = charts.Count - 1;
            InitializeContents();
            
        }

        private void InitializeContents()
        {
            musicNameTextComponent.text = CurrentChart.musicName;
            difficultyTextComponent.text = CurrentChart.difficulty;

            musicCoverImage.sprite = CurrentChart.musicCover;
            cycleImage.sprite = CurrentChart.cycleSprite;

            musicSource.Stop();
            musicSource.clip = CurrentChart.musicClip;
            musicSource.time = CurrentChart.musicPreviewTime;
            musicSource.Play();
        }
    }
}
