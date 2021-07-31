using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

namespace KaitoMajima
{
    public class Score : MonoBehaviour
    {
        public HealthState healthState;

        [SerializeField] private float regenerationStep = 2.5f;
        [SerializeField] private int healthRegenerated = 1;
        [SerializeField] private TextMeshProUGUI scoreTextComponent;
        [SerializeField] private TextMeshProUGUI percentageTextComponent;

        [SerializeField] private Chart musicChart;

        [SerializeField] private Sprite ZScore;

        [SerializeField] private Sprite SS_Score;

        [SerializeField] private Sprite S_Score;

        [SerializeField] private Sprite AScore;

        [SerializeField] private Sprite BScore;

        [SerializeField] private Sprite CScore;

        public static Action<int> onScoreUpdated;
        public static Action<int> onNoteAmountInitiated;

        public static Action onRegenerate;

        public static Action<float> onNoteFail;

        public int noteAmount = 1;

        private float noteHitRate = 1;
        private bool lost;

        private ScoreSet score = new ScoreSet();

        private float currentUpdateTime = 0;
        private void Awake()
        {
            UpdateScore(0);

            onScoreUpdated += UpdateScore;
            onNoteAmountInitiated += SetNoteAmount;
            onNoteFail += FailNote;
            TowersRhythmController.onLastNote += SubmitScore;
        }

        private void SubmitScore()
        {
            musicChart.currentScore.score = score.scoreValue;
            musicChart.currentScore.percentage = score.percentage;

            if(score.percentage == 100)
                musicChart.currentScore.gradeSprite = ZScore;
            else if(score.percentage > 99)
                musicChart.currentScore.gradeSprite = SS_Score;
            else if(score.percentage > 95)
                musicChart.currentScore.gradeSprite = S_Score;
            else if(score.percentage > 85)
                musicChart.currentScore.gradeSprite = AScore;
            else if(score.percentage > 70)
                musicChart.currentScore.gradeSprite = BScore;
            else
                musicChart.currentScore.gradeSprite = CScore;

        }

        private void Update()
        {
            if(lost)
                return;
            currentUpdateTime += Time.deltaTime;
            if(currentUpdateTime >= regenerationStep)
            {
                if(healthState.Health >= healthState.MaxHealth)
                    return;
                currentUpdateTime = 0;
                Health.Heal(ref healthState, healthRegenerated);
                onRegenerate?.Invoke();
            }
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
            if(lost)
                return;
            
            if(amount >= 1)
            {
                if(healthState.Health > 0)
                    healthState.Health -= (int)amount;
                if(healthState.Health <= 0)
                    Lose();
            }

            currentUpdateTime = 0;
            noteHitRate -= amount;
            score.percentage = noteHitRate / noteAmount * 100;
            percentageTextComponent.text = PercentageFormat(score.percentage);
        }

        private void Lose()
        {
            GameManager.OnGameOver?.Invoke();
            lost = true;
            print("lost");
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
            TowersRhythmController.onLastNote -= SubmitScore;
        }
    }

    public class ScoreSet
    {
        public int scoreValue = 0;
        public float percentage = 100;

        
    }
    
}
