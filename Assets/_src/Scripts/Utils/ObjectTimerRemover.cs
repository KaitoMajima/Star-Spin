using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    public class ObjectTimerRemover : MonoBehaviour
    {
        [SerializeField] private bool autoInitialize;
        [SerializeField] private float autoRemoveTime = 0.5f;

        private Coroutine timerCoroutine;
        void Start()
        {
            if(autoInitialize)
                timerCoroutine = StartCoroutine(RemoveTimer(autoRemoveTime));
        }

        private IEnumerator RemoveTimer(float seconds)
        {
            yield return new WaitForSeconds(seconds);
            Destroy(gameObject);
        }

        public void Remove(float seconds)
        {
            timerCoroutine = StartCoroutine(RemoveTimer(seconds));
        }
    }
}
