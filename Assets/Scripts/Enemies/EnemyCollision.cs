using Events;
using UnityEngine;

namespace Enemies
{
    public class EnemyCollision : MonoBehaviour
    {
        public static event EventManager.IntegerEventHandler EnemyDie;

        [SerializeField] private int reward;
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            EnemyDie?.Invoke(reward);
            
            Destroy(gameObject);
        }
    }
}
