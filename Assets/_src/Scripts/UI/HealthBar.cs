using UnityEngine.UI;
using UnityEngine;
using TMPro;
using System.Collections;
using Sirenix.OdinInspector;
using System;

namespace KaitoMajima
{
    public class HealthBar : MonoBehaviour
    {
        [Required][SerializeField] private Score score;
        [SerializeField] private Image barFill;
        [SerializeField] private Image barDamage;
        //[SerializeField] private TMP_Text healthValue;

        [SerializeField] private float barDamageFreezeTime = 0.5f;
        [SerializeField] private float barDamageShrinkSpeed = 1;
        private float barDamageTimer;
        private IEnumerator damagedTimer;
        

        private void Start()
        {
            SetHealth(score.healthState.Health, score.healthState.MaxHealth);
            barDamage.fillAmount = barFill.fillAmount;
            Score.onNoteFail += InflictDamage;
            Score.onRegenerate += Heal;
            Debug.Log("a");
        }

        private void Heal()
        {
            SetHealth(score.healthState.Health, score.healthState.MaxHealth);
        }

        private void InflictDamage(float failValue)
        {
            if(failValue < 1)
                return;
            if (damagedTimer != null)
                StopCoroutine(damagedTimer);
            damagedTimer = DamagedTimer();
            StartCoroutine(damagedTimer);
            SetHealth(score.healthState.Health, score.healthState.MaxHealth);
        }
        private void SetHealth(int health, int maxHealth)
        {
            barFill.fillAmount = GetNormalizedHealth(health, maxHealth);
            //healthValue.text = $"{health.ToString()}/{maxHealth.ToString()}";
        }

        private float GetNormalizedHealth(int currentHealth, int maxHealth)
        {
            return (float)currentHealth / maxHealth;
        }

        private IEnumerator DamagedTimer()
        {
            barDamageTimer = barDamageFreezeTime;
            yield return new WaitForSeconds(barDamageTimer);
            while (true) 
            {

                if(barFill.fillAmount < barDamage.fillAmount)
                {
                    barDamage.fillAmount -= barDamageShrinkSpeed * Time.deltaTime;
                    yield return null;
                }
                else
                {
                    break;
                }
                
            }

        }
        private void OnDestroy()
        {
            Score.onNoteFail -= InflictDamage;
            Score.onRegenerate -= Heal;
        }
        
    }

}
