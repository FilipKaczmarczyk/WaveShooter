using UnityEngine;

namespace Enemies.ScriptableObjects
{
    public enum EnemyType
    {
        Gunship,
        Corvette,
        Frigate
    }
    
    [CreateAssetMenu(menuName = "WaveShooter/EnemyData")]
    public class EnemyDataSO : ScriptableObject
    {
        public EnemyType enemyType;
        public GameObject enemyPrefab;
    }
}
