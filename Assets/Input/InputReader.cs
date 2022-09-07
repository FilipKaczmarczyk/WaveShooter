using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    // We want only one input reader
    //[CreateAssetMenu(menuName = "WaveShooter/InputReader")]
    public class InputReader : ScriptableObject
    {
        [Header("Gameplay")]
        public InputActionReference gameplayShoot;
        public InputActionReference gameplayMousePosition;

        public static void Register(Action<InputAction.CallbackContext> onInput, InputActionReference inputActionReference)
        {
            inputActionReference.action.started += onInput;
            inputActionReference.action.performed += onInput;
            inputActionReference.action.canceled += onInput;
        }

        public static void Unregister(Action<InputAction.CallbackContext> onInput, InputActionReference inputActionReference)
        {
            inputActionReference.action.started -= onInput;
            inputActionReference.action.performed -= onInput;
            inputActionReference.action.canceled -= onInput;
        }
    }
}
