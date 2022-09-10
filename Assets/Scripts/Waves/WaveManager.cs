using System;
using System.Collections.Generic;
using Enemies;
using Events;
using UnityEngine;

namespace Waves
{
    public class WaveManager : MonoBehaviour
    {
        public static event EventManager.VoidEventHandler StartPrepareState;
        public static event EventManager.VoidEventHandler StartFightingState;

        public enum WaveState
        {
            Fighting,
            Prepare
        }

        public enum EnemyType
        {
            
        }
        
        [Serializable]
        public class Wave
        {
            public List<GameObject> enemyTypes;
            public int enemiesCount;
            public float enemiesSpeedMultiplier;
        }

        [Header("Waves values")]
        [SerializeField] private List<Wave> waves;
        [SerializeField] private float timeBetweenWaves = 10f;
        
        [Header("UI references")]
        [SerializeField] private UIWaveTrackerPanel uiWaveTrackerPanel;
        
        [Header("Other references")]
        [SerializeField] private EnemySpawner enemySpawner;
        
        private int _currentWave = 0;
        private WaveState _currentWaveState = WaveState.Fighting;
        private int _currentEnemyCount;
        private float _prepareTimer;
        
        private void OnEnable()
        {
            EnemyCollision.EnemyDie += RemoveEnemy;
        }
    
        private void OnDisable()
        {
            EnemyCollision.EnemyDie -= RemoveEnemy;
        }
        
        private void Start()
        {
            SpawnWave();
        }

        private void Update()
        {
            UpdatePrepareState();
        }

        private void UpdatePrepareState()
        {
            if (_currentWaveState != WaveState.Prepare)
                return;
            
            _prepareTimer -= Time.deltaTime;

            uiWaveTrackerPanel.UpdateTimeToNextWaveText(_prepareTimer);

            if (_prepareTimer <= 0f)
            {
                ChangeState(WaveState.Fighting);
            }
        }

        private void ChangeState(WaveState waveState)
        {
            _currentWaveState = waveState;
            
            if (waveState == WaveState.Fighting)
            {
                SetupFightingState();
            }
            else
            {
                SetupPrepareState();
            }
        }

        private void SetupFightingState()
        {
            StartFightingState?.Invoke();
                
            SpawnWave();
        }
        
        private void SetupPrepareState()
        {
            StartPrepareState?.Invoke();
            
            _prepareTimer = timeBetweenWaves;
        }

        private void SpawnWave()
        {
            enemySpawner.SpawnEnemies(waves[_currentWave]);

            UpdateCurrentEnemyCount(waves[_currentWave].enemiesCount);
        }

        private void RemoveEnemy()
        {
            UpdateCurrentEnemyCount(-1);
            
            uiWaveTrackerPanel.UpdateEnemiesCountText(_currentEnemyCount);

            if (IsWaveEnd())
            {
                SetNextWave();
            }
        }
        
        private void SetNextWave()
        {
            if (_currentWave == waves.Count - 1)
            {
                return;
            }
            
            _currentWave++;
            
            ChangeState(WaveState.Prepare);
        }
        
        private void UpdateCurrentEnemyCount(int amount)
        {
            _currentEnemyCount += amount;
            
            uiWaveTrackerPanel.UpdateEnemiesCountText(_currentEnemyCount);
        }

        private bool IsWaveEnd()
        {
            return _currentEnemyCount == 0;
        }
        
        public int GetCurrentWave()
        {
            return _currentWave;
        }

        public int GetWavesCount()
        {
            return waves.Count;
        }

        public WaveState GetCurrentWaveState()
        {
            return _currentWaveState;
        }
    }
}
