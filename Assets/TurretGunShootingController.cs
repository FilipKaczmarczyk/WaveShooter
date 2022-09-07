using System.Collections.Generic;
using UnityEngine;

public class TurretGunShootingController : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private List<Transform> firePoints;
    
    private void Fire()
    {
        foreach (var firePoint in firePoints)
        {
            Instantiate(bulletPrefab, firePoint);
        }
    }
}
