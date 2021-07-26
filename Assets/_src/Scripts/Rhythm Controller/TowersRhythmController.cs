using System;
using System.Collections;
using System.Collections.Generic;
using SonicBloom.Koreo;
using SonicBloom.Koreo.Demos;
using UnityEngine;

namespace KaitoMajima
{
    public class TowersRhythmController : MonoBehaviour
    {
        [SerializeField] private TransformReference musicSourceDynamicReference;
        private AudioSource musicAudioSource;
        [SerializeField] private TowerBrain[] towerBrains;

        private int currentTowerIndex;

        public TowerBrain CurrentTower
        {
            get
            {
                return towerBrains[currentTowerIndex];
            }
        }
        [SerializeField] private GameObject circleRingPrefab;
        [SerializeField] private GameObject squareRingPrefab;

        [SerializeField] private List<LaneController> laneControllers;
        [EventID] public string eventID;
        private float leadInTime;
        private float leadInTimeLeft;

        [SerializeField] private RingTimingOptions timingOptions;

        public int DelayedSampleTime
		{
			get
			{
				return playingKoreo.GetLatestSampleTime() - (int)(musicAudioSource.pitch * leadInTimeLeft * SampleRate);
			}
		}

        public int SampleRate
        {
            get
            {
                return playingKoreo.SampleRate;
            }
        }

        public int Delay
        {
            get
            {
                return (int)(leadInTime * SampleRate);
            }
        }
        public int SpawningTime
        {
            get
            {
                return DelayedSampleTime - Delay;
            }
        }
        private Koreography playingKoreo;
        [SerializeField] private List<KoreographyEvent> rawKoreographyEvents;

        private int koreographyEventIndex = 0;

        public KoreographyEvent CurrentKoreographyEvent
        {
            get
            {
                return rawKoreographyEvents[koreographyEventIndex];
            }
        }
        private const int CIRCLE_BACK = 0;
        private const int SQUARE_BACK = 1;
        private const int CIRCLE_NEUTRAL = 2;
        private const int SQUARE_NEUTRAL = 3;
        private const int CIRCLE_FORWARD = 4;
        private const int SQUARE_FORWARD = 5;



        private void Start()
        {
            musicAudioSource = musicSourceDynamicReference.Value.GetComponent<AudioSource>();
            InitializeWave();
        }

        private void InitializeWave()
        {
            currentTowerIndex = 0;

            leadInTime = timingOptions.perfectWindow;
            leadInTimeLeft = leadInTime;

            playingKoreo = Koreographer.Instance.GetKoreographyAtIndex(0);

            KoreographyTrackBase rhythmTrack = playingKoreo.GetTrackByID(eventID);
			rawKoreographyEvents = rhythmTrack.GetAllEvents();

        }

        private void Update()
        {
            if(leadInTimeLeft > 0)
                leadInTimeLeft -= Time.deltaTime;
            else
                if(!musicAudioSource.isPlaying) musicAudioSource.Play();
            
            if(koreographyEventIndex >= rawKoreographyEvents.Count)
                return;
            if(DelayedSampleTime >= CurrentKoreographyEvent.StartSample - Delay)
            {
                TriggerNote(CurrentKoreographyEvent);
                koreographyEventIndex++;
                
            }
        }
        private void TriggerNote(KoreographyEvent koreoEvent)
        {

            switch(koreoEvent.GetIntValue())
            {
                case CIRCLE_BACK:
                    RegressTower();
                    SpawnRing(circleRingPrefab);
                    break;
                case CIRCLE_NEUTRAL:
                    SpawnRing(circleRingPrefab);
                    break;
                case CIRCLE_FORWARD:
                    AdvanceTower();
                    SpawnRing(circleRingPrefab);
                    break;
                case SQUARE_BACK:
                    RegressTower();
                    SpawnRing(squareRingPrefab);
                    break;
                case SQUARE_NEUTRAL:
                    SpawnRing(squareRingPrefab);
                    break;
                case SQUARE_FORWARD:
                    AdvanceTower();
                    SpawnRing(squareRingPrefab);
                    break;
            }
        }

        private void AdvanceTower()
        {
            currentTowerIndex++;
            if(currentTowerIndex >= towerBrains.Length)
                currentTowerIndex = 0;
        }

        private void RegressTower()
        {
            currentTowerIndex--;
            if(currentTowerIndex < 0)
                currentTowerIndex = towerBrains.Length - 1;
        }

        private void SpawnRing(GameObject ringPrefab)
        {
            var ring = Instantiate(ringPrefab, CurrentTower.ringTowerTransform.position, ringPrefab.transform.rotation, CurrentTower.ringTowerTransform);
            var ringScript = ring.GetComponent<RingDetection>();

            CurrentTower.AddRing(ringScript);
        }
    }
}