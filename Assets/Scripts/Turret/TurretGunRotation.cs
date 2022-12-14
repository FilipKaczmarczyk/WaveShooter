using Input;
using Input.ScriptableObject;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Turret
{
    public class TurretGunRotation : MonoBehaviour
    {
        [SerializeField] private InputReaderSO inputReaderSo;
    
        private Vector2 _mouseScreenPosition;
    
        private void OnEnable()
        {
            InputReaderSO.Register(OnMousePosition, inputReaderSo.gameplayMousePosition);
        }
    
        private void OnDisable()
        {
            InputReaderSO.Unregister(OnMousePosition, inputReaderSo.gameplayMousePosition);
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
