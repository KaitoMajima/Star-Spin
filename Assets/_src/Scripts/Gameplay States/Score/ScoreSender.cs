using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    public class ScoreSender : MonoBehaviour
    {
        [SerializeField] private SpawnObject objectSpawner;
        public void SendScore(int noteScore)
        {
            Score.onScoreUpdated?.Invoke(noteScore);
            var feedbackScore = objectSpawner.Spawn();
            var feedbackScript = feedbackScore.GetComponent<ScoreFeedback>();
            feedbackScript.SetScore(noteScore);
        }
    }
}
