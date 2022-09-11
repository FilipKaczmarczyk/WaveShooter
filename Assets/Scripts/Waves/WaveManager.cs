using System;
using System.Collections.Generic;
using System.Linq;
using Enemies;
using Enemies.ScriptableObjects;
using Events;
using UI;
using UnityEngine;

namespace Waves
{
    public class WaveManager : MonoBehaviour
    {
        public static event EventManager.VoidEventHandler StartPrepareState;
        public static event EventManager.VoidEventHandler StartFightingState;

        public static event EventManager.VoidEventHandler CompletedAllWaves;

        public enum WaveState
        {
            Fighting,
            Prepare
        }
        
        [Serializable]
        public class EnemyToSpawnInfo
        {
            public EnemyType enemyType;
            public int enemyCount;
        }
        
        [Serializable]
        public class Wave
        {
            public List<EnemyToSpawnInfo> enemyToSpawnInfos;
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
            EnemyController.EnemyDie += RemoveEnemy;
        }
    
        private void OnDisable()
        {
            EnemyController.EnemyDie -= RemoveEnemy;
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

            var enemiesCount = waves[_currentWave].enemyToSpawnInfos.Sum(enemyToSpawnInfo => enemyToSpawnInfo.enemyCount);

            UpdateCurrentEnemyCount(enemiesCount);
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
                CompletedAllWaves?.Invoke();
                    
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
