using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KaitoMajima
{
    public class GameWinCharts : MonoBehaviour
    {
        [SerializeField] private Score score;

        [SerializeField] private Chart chart;
        private ScoreSet scoreSet;
        [SerializeField] private TextMeshProUGUI musicNameTextComponent;
        [SerializeField] private GameObject personalBest;
        [SerializeField] private TextMeshProUGUI scoreTextComponent;
        [SerializeField] private TextMeshProUGUI percentageTextComponent;
        [SerializeField] private Image gradeImage;

        [SerializeField] private TextMeshProUGUI perfectTextComponent;
        [SerializeField] private TextMeshProUGUI earlyTextComponent;

        [SerializeField] private TextMeshProUGUI lateTextComponent;

        [SerializeField] private TextMeshProUGUI misfireTextComponent;

        [SerializeField] private TextMeshProUGUI missTextComponent;


        private void Start()
        {
            scoreSet = score.score;
            musicNameTextComponent.text = chart.musicName;
            personalBest.SetActive(scoreSet.newPersonalBest);
            scoreTextComponent.text = String.Format("{0:D8}", scoreSet.scoreValue);
            percentageTextComponent.text = PercentageFormat(scoreSet.percentage);
            gradeImage.sprite = scoreSet.gradeSprite;

            perfectTextComponent.text = scoreSet.perfectNotes.ToString();
            earlyTextComponent.text = scoreSet.earlyNotes.ToString();
            lateTextComponent.text = scoreSet.lateNotes.ToString();
            misfireTextComponent.text = scoreSet.misfiredNotes.ToString();
            missTextComponent.text = scoreSet.missedNotes.ToString();
        }

        private string PercentageFormat(float value)
        {
            string text = String.Format("{0:0.00}", value);

            if(text.EndsWith("00"))
                return ((int)value).ToString() + "%";
            
            return text + "%";
        }


    }
}
