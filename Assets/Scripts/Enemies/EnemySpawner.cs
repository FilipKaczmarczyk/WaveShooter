using System.Collections.Generic;
using Enemies.ScriptableObjects;
using UnityEngine;
using Waves;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private AllTypesOfEnemyDataSO allTypesOfEnemyData;
        [SerializeField] private float spawnRadius = 30f;
        
        public void SpawnEnemies(WaveManager.Wave wave)
        {
            foreach (var enemyToSpawnInfo in wave.enemyToSpawnInfos)
            {
                for (var i = 0; i < enemyToSpawnInfo.enemyCount; i++)
                {
                    var enemyData = allTypesOfEnemyData.enemies.Find(x => x.enemyType == enemyToSpawnInfo.enemyType);
                    
                    var enemyGO = Instantiate(enemyData.enemyPrefab, RandomOnCircle(Vector3.zero, spawnRadius), Quaternion.identity);

                    enemyGO.GetComponent<EnemyController>().Init(wave.enemiesSpeedMultiplier);
                }
            }
        }

        private Vector3 RandomOnCircle(Vector2 center, float radius)
        {
            var diameter = radius * 2;
            var potentialSpawnPosition = center + Random.insideUnitCircle * diameter;

            if ((potentialSpawnPosition - Vector2.zero).sqrMagnitude <= radius * radius)
            {
                return RandomOnCircle(center, radius);
            }

            return potentialSpawnPosition;
        }
    }
}
