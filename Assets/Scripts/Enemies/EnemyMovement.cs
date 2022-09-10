using UnityEngine;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private float speed;

        private Rigidbody2D _rigidbody;

        public void Init(float speedModifier)
        {
            var lookDirection = Vector2.zero - (Vector2) transform.position;
            var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.AddForce(speed * transform.up * speedModifier, ForceMode2D.Impulse);
        }
    }
}
