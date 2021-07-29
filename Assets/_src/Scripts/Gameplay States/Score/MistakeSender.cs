using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    public class MistakeSender : MonoBehaviour
    {
        public void SendMistakeValue(float failValue)
        {
            Score.onNoteFail?.Invoke(failValue);
        }
    }
}
