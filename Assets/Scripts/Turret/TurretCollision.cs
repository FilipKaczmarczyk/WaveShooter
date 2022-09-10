using Turret;
using UnityEngine;

public class TurretCollision : MonoBehaviour
{
    [SerializeField] private TurretHealth turretHealth;
    
    private const int TurretDamageTaken = 20;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        turretHealth.AddTurretHealth(-TurretDamageTaken);
    }
}
