using UnityEngine;

public class TurretGunRotateController : MonoBehaviour
{
    private void Update()
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        var lookDirection = mousePosition - (Vector2)transform.position;
        var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
