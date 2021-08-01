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

        public static Action<int> onNoteRegister;

        public enum NoteType
        {
            Perfect = 0,
            Early = 1,
            Late = 2,
            Misfire = 3,
            Miss = 4
        }
        public int noteAmount = 1;
        [SerializeField] private int notesPassed;
        private float noteHitRate = 1;
        private bool lost;

        public ScoreSet score = new ScoreSet();

        private float currentUpdateTime = 0;
        private void Awake()
        {
            UpdateScore(0);

            onScoreUpdated += UpdateScore;
            onNoteRegister += RegisterScore;
            onNoteAmountInitiated += SetNoteAmount;
            onNoteFail += FailNote;
        }

        private void RegisterScore(int note)
        {
            var noteType = (NoteType)note;
            switch(noteType)
            {
                case NoteType.Perfect:
                    score.perfectNotes++;
                    break;
                case NoteType.Early:
                    score.earlyNotes++;
                    break;
                case NoteType.Late:
                    score.lateNotes++;
                    break;
                case NoteType.Misfire:
                    score.misfiredNotes++;
                    break;
                case NoteType.Miss:
                    score.missedNotes++;
                    break;
               
            }
            if(notesPassed == noteAmount)
                SubmitScore();
        }

        private void SubmitScore()
        {   
            if(score.percentage == 100)
            {
                SetGrade(ref score.gradeSprite, ZScore);
            }
                
            else if(score.percentage > 99)
            {
                SetGrade(ref score.gradeSprite, SS_Score);
            }
            else if(score.percentage > 95)
            {
                SetGrade(ref score.gradeSprite, S_Score);
            }
            else if(score.percentage > 85)
            {
                SetGrade(ref score.gradeSprite, AScore);
            }
            else if(score.percentage > 70)
            {
                SetGrade(ref score.gradeSprite, BScore);
            }
            else
            {
                SetGrade(ref score.gradeSprite, CScore);
            }

            if(musicChart.currentScore.gradeSprite != null)
            {
                int previousScore = musicChart.currentScore.score;
                if(previousScore > score.scoreValue)
                    return;

            }
            score.newPersonalBest = true;

            musicChart.currentScore = new Chart.ChartScore();

            musicChart.currentScore.score = score.scoreValue;
            musicChart.currentScore.percentage = score.percentage;

            musicChart.currentScore.perfectNotes = score.perfectNotes;
            musicChart.currentScore.earlyNotes = score.earlyNotes;
            musicChart.currentScore.lateNotes = score.misfiredNotes;
            musicChart.currentScore.misfiredNotes = score.misfiredNotes;
            musicChart.currentScore.missedNotes = score.missedNotes;

            musicChart.currentScore.gradeSprite = score.gradeSprite;

        }

        private void SetGrade(ref Sprite receivingSprite, Sprite spriteSender)
        {
            receivingSprite = spriteSender;
        }
        private void UpdateScore(int noteValue)
        {
            notesPassed++;
            score.scoreValue += noteValue;
            score.percentage = noteHitRate / noteAmount * 100;
            scoreTextComponent.text = String.Format("{0:D8}", score.scoreValue);
            percentageTextComponent.text = PercentageFormat(score.percentage);

            if(healthState.Health >= healthState.MaxHealth)
                return;
            
            Health.Heal(ref healthState, healthRegenerated);
            onRegenerate?.Invoke();
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
                notesPassed++;
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
            onNoteRegister -= RegisterScore;
            onNoteFail -= FailNote;
        }
    }

    [Serializable]
    public class ScoreSet
    {
        public int scoreValue = 0;
        public float percentage = 100;
        public bool newPersonalBest;
        public int perfectNotes;
        public int earlyNotes;
        public int lateNotes;
        public int misfiredNotes;
        public int missedNotes;
        public Sprite gradeSprite;
    }
    
}
