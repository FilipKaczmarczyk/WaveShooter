using System.Collections.Generic;
using PlayerData.ScriptableObjects;
using UI;
using UnityEngine;

namespace AutoTurret
{
    public class AutoTurretsController : MonoBehaviour
    {
        [SerializeField] private GameObject autoTurretPrefab;
        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] private List<Transform> spawnPoints;

        private void OnEnable()
        {
            UIUpgradePanel.SpawnAutoTurret += SpawnTurret;
        }

        private void OnDisable()
        {
            UIUpgradePanel.SpawnAutoTurret -= SpawnTurret;
        }

        private void SpawnTurret()
        {
            Instantiate(autoTurretPrefab, spawnPoints[playerData.AutoTurretCount].position, Quaternion.identity);

            playerData.AutoTurretCount ++;
        }
    }
}
