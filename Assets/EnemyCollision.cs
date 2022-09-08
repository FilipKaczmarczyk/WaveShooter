using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }
}
