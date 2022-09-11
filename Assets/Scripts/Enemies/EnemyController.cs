using Bullet;
using Events;
using PlayerData;
using PlayerData.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Enemies
{
    public class EnemyController : MonoBehaviour
    {
        public static event EventManager.VoidEventHandler EnemyDie;

        [SerializeField] private Transform enemyVisual;
        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] private int maxHealth;
        [SerializeField] private GameObject heathBar;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private float speed;
        [SerializeField] private int damage;
        [SerializeField] private int reward;
        [SerializeField] private GameObject destroyPrefab;
 
        private Rigidbody2D _rigidbody;
        private int _currentHeath;
        private GameObject _lastCollidedObject;
        private bool _died;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag($"Bullet"))
            {
                if (other.gameObject == _lastCollidedObject)
                {
                    return;
                }

                _lastCollidedObject = other.gameObject;
            
                Destroy(other.gameObject);
            
                TakeDamage();
            }

            if (other.gameObject.CompareTag($"Player"))
            {
                Die();
            }
        }
        
        public void Init(float speedModifier)
        {
            var lookDirection = Vector2.zero - (Vector2) transform.position;
            var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            _rigidbody = transform.GetComponent<Rigidbody2D>();
            _rigidbody.AddForce(speed * enemyVisual.up * speedModifier, ForceMode2D.Impulse);
            
            _currentHeath = maxHealth;
        }

        public int GetDamage()
        {
            return damage;
        }
        
        public void TakeDamage()
        {
            _currentHeath -= playerData.CurrentSelectedAmmo.upgradePrefab.GetComponent<BulletController>().Damage;

            if (_currentHeath <= 0)
            {
                Die();
            }
            else
            {
                if (!heathBar.activeInHierarchy)
                {
                    heathBar.SetActive(true);
                }
            
                var healthPercentValue = (float)_currentHeath / maxHealth;

                healthSlider.value = healthPercentValue;
            }
        }

        private void Die()
        {
            if (_died) return;

            _died = true;
            
            PlayerMoneyHandler.AddMoney(reward);
            
            EnemyDie?.Invoke();
            
            Instantiate(destroyPrefab, transform.position, Quaternion.identity);
            
            Destroy(gameObject);
        }
    }
}
