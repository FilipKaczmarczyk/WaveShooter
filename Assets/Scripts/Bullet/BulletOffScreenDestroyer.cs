using UnityEngine;

public class BulletOffScreenDestroyer : MonoBehaviour
{
    private void Update() 
    {
        Vector2 screenPosition = Camera.main.WorldToScreenPoint(transform.position);
        
        if (screenPosition.y > Screen.height || screenPosition.y < 0 || screenPosition.x > Screen.width || screenPosition.x < 0)
        {
            Destroy(gameObject);
        }
    }
}
