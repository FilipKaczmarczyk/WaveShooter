using System;
using Events;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public class GlobalInputManager : MonoBehaviour
    {
        public enum ActionMaps
        {
            Gameplay
        }
        
        [SerializeField] private PlayerInput playerInput;
        
        private const string ActionMapGameplay = "Gameplay";

        private static event EventManager.ActionMapEventHandler ChangeActionMapControls;
        private static event EventManager.VoidEventHandler DisableActionMaps;

        private void OnEnable()
        {
            ChangeActionMapControls += OnChangeActionMap;

            DisableActionMaps += DisableAllActionMaps;
        }

        private void OnDisable()
        {
            ChangeActionMapControls -= OnChangeActionMap;
            
            DisableActionMaps -= DisableAllActionMaps;
        }

        private void OnChangeActionMap(ActionMaps actionMap)
        {
            DisableAllActionMaps();

            switch (actionMap)
            {
                case ActionMaps.Gameplay:
                    playerInput.actions.FindActionMap(ActionMapGameplay).Enable();
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException(nameof(actionMap), actionMap, null);
            }
        }
        
        private void DisableAllActionMaps()
        {
            if (!playerInput)
                playerInput = GetComponent<PlayerInput>();
            
            foreach (var playerInputAction in playerInput.actions)
            {
                playerInputAction.Disable();
            }
        }
        
        private void Start()
        {
            InvokeChangeActionMapControls(ActionMaps.Gameplay);
        }

        public static void InvokeChangeActionMapControls(ActionMaps actionMaps)
        {
            ChangeActionMapControls?.Invoke(actionMaps);
        }

        public static void DisableAllActionMapControls()
        {
            DisableActionMaps?.Invoke();
        }
    }
}
