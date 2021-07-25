using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KaitoMajima
{
    public class RandomRingTrigger : MonoBehaviour
    {
        [SerializeField] private Transform ringTowerTransform;

        [SerializeField] private TowerBrain towerBrain;

        [SerializeField] private GameObject ringPrefab;
        
        [SerializeField] private Vector2 minMaxIntervalRange = new Vector2(0.15f, 0.5f);

        private Coroutine ringSpawnCoroutine;
        private void Start()
        {
            ringSpawnCoroutine = StartCoroutine(SpawnCoroutineTimer());
        }

        private IEnumerator SpawnCoroutineTimer()
        {
            float waitingTime = Random.Range(minMaxIntervalRange.x, minMaxIntervalRange.y);

            yield return new WaitForSeconds(waitingTime);

            var ring = Instantiate(ringPrefab, ringTowerTransform.position, ringPrefab.transform.rotation, ringTowerTransform);
            var ringScript = ring.GetComponent<RingDetection>();

            towerBrain.AddRing(ringScript);
            
            ringSpawnCoroutine = StartCoroutine(SpawnCoroutineTimer());
        }
    }
}
