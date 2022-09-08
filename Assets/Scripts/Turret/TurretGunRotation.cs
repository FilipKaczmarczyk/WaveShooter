using Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Turret
{
    public class TurretGunRotation : MonoBehaviour
    {
        [SerializeField] private InputReader inputReader;
    
        private Vector2 _mouseScreenPosition;
    
        private void OnEnable()
        {
            InputReader.Register(OnMousePosition, inputReader.gameplayMousePosition);
        }
    
        private void OnDisable()
        {
            InputReader.Unregister(OnMousePosition, inputReader.gameplayMousePosition);
        }
    
        private void OnMousePosition(InputAction.CallbackContext ctx)
        {
            _mouseScreenPosition = ctx.ReadValue<Vector2>();
        }

        private void Update()
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(_mouseScreenPosition);

            var lookDirection = mousePosition - (Vector2)transform.position;
            var angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
            transform.rotation = Quaternion.Euler(0, 0, angle);
        }
    }
}
