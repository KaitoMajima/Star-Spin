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
        public static Action<int> onNoteAmountInitiated;

        public static Action<float> onNoteFail;

        public int noteAmount = 1;

        private float noteHitRate = 1;

        private ScoreSet score = new ScoreSet();

        private void Awake()
        {
            UpdateScore(0);

            onScoreUpdated += UpdateScore;
            onNoteAmountInitiated += SetNoteAmount;
            onNoteFail += FailNote;
        }

        
        private void UpdateScore(int noteValue)
        {
            score.scoreValue += noteValue;
            score.percentage = noteHitRate / noteAmount * 100;
            scoreTextComponent.text = String.Format("{0:D8}", score.scoreValue);
            percentageTextComponent.text = PercentageFormat(score.percentage);
        }

        private void SetNoteAmount(int amount)
        {
            noteAmount = amount;
            noteHitRate = noteAmount;
        }

        private void FailNote(float amount)
        {
            noteHitRate -= amount;
            score.percentage = noteHitRate / noteAmount * 100;
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
            onNoteAmountInitiated -= SetNoteAmount;
            onNoteFail -= FailNote;
        }
    }

    public class ScoreSet
    {
        public int scoreValue = 0;
        public float percentage = 100;
    }
    
}
