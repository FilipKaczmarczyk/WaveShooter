using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input.ScriptableObject
{
    // We want only one input reader
    //[CreateAssetMenu(menuName = "WaveShooter/InputReader")]
    public class InputReaderSO : UnityEngine.ScriptableObject
    {
        [Header("Gameplay")]
        public InputActionReference gameplayShoot;
        public InputActionReference gameplayMousePosition;
        public InputActionReference gameplayPause;
        
        [Header("Pause")]
        public InputActionReference pauseReturn;

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
