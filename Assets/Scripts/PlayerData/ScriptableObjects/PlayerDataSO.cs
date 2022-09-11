using System;
using UnityEngine;
using Upgrades.ScriptableObjects;

namespace PlayerData.ScriptableObjects
{
    // We want only one player data
    [CreateAssetMenu(menuName = "WaveShooter/PlayerData")]
    public class PlayerDataSO : ScriptableObject
    {
        public UpgradeSO CurrentSelectedGun { get; set; }
        public UpgradeSO CurrentSelectedAmmo { get; set; }
        public int AutoTurretCount { get; set; }
        
        public UpgradeSO autoTurret;
        public int maxTurretCount;
        
        [SerializeField] private UpgradeSO basicGun;
        [SerializeField] private UpgradeSO basicAmmo;

        public void Reset()
        {
            CurrentSelectedGun = basicGun;
            CurrentSelectedAmmo = basicAmmo;
            AutoTurretCount = 0;
            PlayerMoneyHandler.SetMoney(0);
        }
    }
}
