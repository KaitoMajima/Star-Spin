using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace KaitoMajima
{
    public class ScoreFeedback : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI scoreTextComponent;

        public void SetScore(int score)
        {
            scoreTextComponent.text = $"+ {score.ToString()}";
        }
    }
}
