using Events;
using PlayerData;
using UnityEngine;

namespace Enemies
{
    public class EnemyCollision : MonoBehaviour
    {
        public static event EventManager.VoidEventHandler EnemyDie;

        [SerializeField] private int reward;

        private bool _alreadyCollided;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (_alreadyCollided) // I used this because on gameObject are multiple colliders
                return;

            _alreadyCollided = true;
            
            EnemyDie?.Invoke();

            if (other.gameObject.CompareTag($"Bullet"))
            {
                PlayerMoneyHandler.AddMoney(reward);
            }

            Destroy(gameObject);
        }
    }
}
