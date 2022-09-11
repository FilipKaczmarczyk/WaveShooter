using UnityEngine;

namespace Upgrades.ScriptableObjects
{
    [CreateAssetMenu(menuName = "WaveShooter/Upgrade")]
    public class UpgradeSO : ScriptableObject
    {
        public string upgradeName;
        public GameObject upgradePrefab;
        public int upgradeCost;
        public Sprite upgradeImage;
        public UpgradeSO nextUpgrade;
    }
}
