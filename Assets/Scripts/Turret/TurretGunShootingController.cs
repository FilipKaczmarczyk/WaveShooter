using System.Collections.Generic;
using Input;
using UnityEngine;
using UnityEngine.InputSystem;

public class TurretGunShootingController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<Transform> firePoints;

    private InputActions _inputActions;

    
    private void OnEnable()
    {
        InputReader.Register(Fire, inputReader.gameplayShoot);
    }
    
    private void OnDisable()
    {
        InputReader.Unregister(Fire, inputReader.gameplayShoot);
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
