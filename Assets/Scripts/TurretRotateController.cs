using UnityEngine;

public class TurretRotateController : MonoBehaviour
{
    [SerializeField] private Transform gunTransform;
    
    private void FixedUpdate()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var lookDirection = mousePosition - (Vector2)gunTransform.position;
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        gunTransform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
