using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace KaitoMajima
{
    public class Score : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTextComponent;
        [SerializeField] private TextMeshProUGUI percentageTextComponent;

        public static Action<int> onScoreUpdated;

        private ScoreSet score = new ScoreSet();

        private void Start()
        {
            UpdateScore(0);

            onScoreUpdated += UpdateScore;
        }

        private void UpdateScore(int noteValue)
        {
            score.scoreValue += noteValue;
            scoreTextComponent.text = String.Format("{0:D8}", score.scoreValue);
            percentageTextComponent.text = PercentageFormat(score.percentage);
        }

        private string PercentageFormat(float value)
        {
            string text = String.Format("{0:0.00}", value);

            if(text.EndsWith("00"))
                return ((int)value).ToString() + "%";
            
            return text + "%";
        }

        private void OnDestroy()
        {
            onScoreUpdated -= UpdateScore;
        }
    }

    public class ScoreSet
    {
        public int scoreValue = 0;
        public float percentage = 100;
    }
    
}
