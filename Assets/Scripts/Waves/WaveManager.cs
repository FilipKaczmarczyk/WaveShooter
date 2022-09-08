using System;
using System.Collections.Generic;
using Enemies;
using Events;
using UnityEngine;

namespace Waves
{
    public class WaveManager : MonoBehaviour
    {
        public static event EventManager.VoidEventHandler WaveChange;
        
        private enum WaveState
        {
            Fighting,
            Prepare
        }
        
        [Serializable]
        public class Wave
        {
            public List<GameObject> enemyTypes;
            public int enemiesCount;
            public float enemiesSpeedMultiplier;
        }

        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private List<Wave> waves;
        [SerializeField] private float timeBetweenWaves = 10f;
        
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
            if (_currentWaveState != WaveState.Prepare)
                return;

            _prepareTimer -= Time.deltaTime;

            if (_prepareTimer <= 0f)
            {
                _currentWaveState = WaveState.Fighting;
                
                SpawnWave();
            }
        }

        private void SpawnWave()
        {
            enemySpawner.SpawnEnemies(waves[_currentWave]);

            AddCurrentEnemyCount(waves[_currentWave].enemiesCount);
        }

        private void RemoveEnemy(int enemyValue)
        {
            _currentEnemyCount--;

            if (IsWaveEnd())
            {
                SetNextWave();
            }
        }
        
        private void AddCurrentEnemyCount(int amount)
        {
            _currentEnemyCount += amount;
        }

        private bool IsWaveEnd()
        {
            return _currentEnemyCount == 0;
        }

        private void SetNextWave()
        {
            _currentWave++;
            
            WaveChange?.Invoke();

            _currentWaveState = WaveState.Prepare;

            _prepareTimer = timeBetweenWaves;
        }

        public int GetCurrentWave()
        {
            return _currentWave;
        }

        public int GetWavesCount()
        {
            return waves.Count;
        }
    }
}
