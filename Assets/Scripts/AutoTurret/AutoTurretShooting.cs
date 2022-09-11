using PlayerData.ScriptableObjects;
using UnityEngine;

namespace AutoTurret
{
    public class AutoTurretShooting : MonoBehaviour
    {
        [SerializeField] private float rotationSpeed = 5f;
        [SerializeField] private float timeBetweenShoots = 1f;
        [SerializeField] private Transform firePoint;
        [SerializeField] private PlayerDataSO playerData;
        private Transform _target;

        private const float Offset = 90f;
        private float _cooldownTimer = 0f;

        private void Awake()
        {
            _cooldownTimer = timeBetweenShoots;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_target != null)
                return;
        
            if (other.gameObject.CompareTag($"Enemy"))
            {
                _target = other.transform;
            }
        }
    
        private void OnTriggerStay2D(Collider2D other)
        {
            if (_target != null)
                return;
        
            if (other.gameObject.CompareTag($"Enemy"))
            {
                _target = other.transform;
            }
        }

        private void Update()
        {
            if (_target == null)
                return;
        
            Aim();

            _cooldownTimer -= Time.deltaTime;
        
            if (_cooldownTimer <= 0f)
            {
                Shoot();
            }
        }

        private void Aim()
        {
            Vector2 direction = _target.position - transform.position;
            direction.Normalize();
        
            var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - Offset;      
        
            var rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }

        private void Shoot()
        {
            Instantiate(playerData.CurrentSelectedAmmo.upgradePrefab, firePoint.position, transform.rotation);

            _target = null;
        
            _cooldownTimer = timeBetweenShoots;
        }
    }
}
