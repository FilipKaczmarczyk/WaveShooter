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
            Gameplay,
            Pause
        }
        
        [SerializeField] private PlayerInput playerInput;
        
        private const string ActionMapGameplay = "Gameplay";
        private const string ActionMapPause = "Pause";

        private static event EventManager.ActionMapEventHandler ChangeActionMapControls;
        private static event EventManager.VoidEventHandler DisableActionMaps;
        
        private static GlobalInputManager _instance;

        void Awake()
        {
            if (_instance == null)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        
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
                
                case ActionMaps.Pause:
                    playerInput.actions.FindActionMap(ActionMapPause).Enable();
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
