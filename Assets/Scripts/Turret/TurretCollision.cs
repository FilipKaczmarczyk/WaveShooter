using Enemies;
using UnityEngine;

namespace Turret
{
    public class TurretCollision : MonoBehaviour
    {
        [SerializeField] private TurretHealth turretHealth;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                var damageToTake = other.gameObject.GetComponent<EnemyController>().GetDamage();
                
                turretHealth.AddTurretHealth(-damageToTake);
            }
        }
    }
}
