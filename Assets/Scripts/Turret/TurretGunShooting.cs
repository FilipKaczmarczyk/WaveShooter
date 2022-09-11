using System.Collections.Generic;
using Input;
using Input.ScriptableObject;
using PlayerData.ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Turret
{
    public class TurretGunShooting : MonoBehaviour
    {
        [SerializeField] private InputReaderSO inputReaderSo;
        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] private List<Transform> firePoints;

        private void OnEnable()
        {
            InputReaderSO.Register(Fire, inputReaderSo.gameplayShoot);
        }
    
        private void OnDisable()
        {
            InputReaderSO.Unregister(Fire, inputReaderSo.gameplayShoot);
        }

        private void Fire(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
        
            foreach (var firePoint in firePoints)
            {
                Instantiate(playerData.CurrentSelectedAmmo.upgradePrefab, firePoint.position, transform.rotation);
            }
        }
    }
}
