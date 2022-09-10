using UnityEngine;

namespace Bullet
{
    public class BulletCollision : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
}
