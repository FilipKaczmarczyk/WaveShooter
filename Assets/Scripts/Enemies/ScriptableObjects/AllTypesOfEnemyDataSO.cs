using System.Collections.Generic;
using UnityEngine;

namespace Enemies.ScriptableObjects
{
    // We want only one all enemies SO
    //[CreateAssetMenu(menuName = "WaveShooter/AllEnemies")]
    public class AllTypesOfEnemyDataSO : ScriptableObject
    {
        public List<EnemyDataSO> enemies;
    }
}
