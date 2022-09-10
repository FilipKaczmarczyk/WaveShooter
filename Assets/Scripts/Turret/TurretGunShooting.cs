using System.Collections.Generic;
using Input;
using Input.ScriptableObject;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Turret
{
    public class TurretGunShooting : MonoBehaviour
    {
        [SerializeField] private InputReaderSO inputReaderSo;
        [SerializeField] private GameObject bulletPrefab;
        [SerializeField] private List<Transform> firePoints;

        private InputActions _inputActions;
        
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
                Instantiate(bulletPrefab, firePoint.position, transform.rotation);
            }
        }
    }
}
