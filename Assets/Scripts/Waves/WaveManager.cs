using System;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

namespace Waves
{
    public class WaveManager : MonoBehaviour
    {
        [Serializable]
        public class Wave
        {
            public List<GameObject> enemyTypes;
            public int enemiesCount;
            public float enemiesSpeedMultiplier;
        }

        [SerializeField] private EnemySpawner enemySpawner;
        [SerializeField] private List<Wave> waves;
    
        private int _currentWave = 0;
        private int _currentEnemyCount;
        
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
            
            SpawnWave();
        }
    }
}
