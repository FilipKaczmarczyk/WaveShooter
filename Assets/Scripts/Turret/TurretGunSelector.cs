using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Turret
{
    public class TurretGunSelector : MonoBehaviour
    {
        [SerializeField] private List<GameObject> gunList;

        private int _currentSelectedGun;
    
        public void OnEnable()
        {
            UIUpgradePanel.GunUpgrade += SelectNextGun;
        }

        private void OnDisable()
        {
            UIUpgradePanel.GunUpgrade -= SelectNextGun;
        }

        private void SelectNextGun()
        {
            gunList[_currentSelectedGun].SetActive(false);

            _currentSelectedGun++;
        
            gunList[_currentSelectedGun].SetActive(true);
        }
    }
}
