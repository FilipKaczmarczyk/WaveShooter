using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> enemiesGameObjects;
        [SerializeField] private float spawnRadius = 30f;
    
        private void Start()
        {
            for (var i = 0; i < 10; i++)
            {
                SpawnEnemy();
            }
        }

        private void SpawnEnemy()
        {
            Instantiate(enemiesGameObjects[0], RandomOnSphere(Vector3.zero, spawnRadius), Quaternion.identity);
        }

        private Vector3 RandomOnSphere(Vector2 center, float radius)
        {
            var diameter = radius * 2;
            var potentialSpawnPosition = center + Random.insideUnitCircle * diameter;

            if ((potentialSpawnPosition - Vector2.zero).sqrMagnitude <= radius * radius)
            {
                return RandomOnSphere(center, radius);
            }

            return potentialSpawnPosition;
        }
    
    
    }
}
