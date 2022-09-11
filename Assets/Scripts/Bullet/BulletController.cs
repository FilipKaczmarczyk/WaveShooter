using UnityEngine;

namespace Bullet
{
    public class BulletController : MonoBehaviour
    {
        [field:SerializeField] public int Damage { get; private set;}
        
        [SerializeField] private float speed;

        private Rigidbody2D _rigidbody;

        private void Start()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _rigidbody.AddForce(speed * transform.up, ForceMode2D.Impulse);
        }
    }
}
