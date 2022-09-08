using System.Collections.Generic;
using UnityEngine;
using Waves;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private float spawnRadius = 30f;
        
        public void SpawnEnemies(WaveManager.Wave wave)
        {
            for (var i = 0; i < wave.enemiesCount; i++)
            {
                var randomEnemy = Random.Range(0, wave.enemyTypes.Count);
                
                Instantiate(wave.enemyTypes[randomEnemy], RandomOnCircle(Vector3.zero, spawnRadius), Quaternion.identity);
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
