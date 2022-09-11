using Input;
using Input.ScriptableObject;
using PlayerData.ScriptableObjects;
using SceneLoading;
using Turret;
using UnityEngine;
using UnityEngine.InputSystem;
using Waves;

namespace UI
{
    public class UICanvasController : MonoBehaviour
    {
        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] private InputReaderSO inputReader;
        [SerializeField] private GameObject upgradesPanel;
        [SerializeField] private GameObject pauseMenu;
        [SerializeField] private GameObject winMenu;
        [SerializeField] private GameObject gameOverMenu;
    
        private void OnEnable()
        {
            WaveManager.StartFightingState += DisableUpgradesPanel;
            WaveManager.StartPrepareState += EnableUpgradesPanel;
            WaveManager.CompletedAllWaves += EnableWinMenu;
            TurretHealth.PlayerDied += EnableGameOverMenu;
            
            InputReaderSO.Register(EnablePauseMenu, inputReader.gameplayPause);
            InputReaderSO.Register(DisablePauseMenu, inputReader.pauseReturn);
        }

        private void OnDisable()
        {
            WaveManager.StartFightingState -= DisableUpgradesPanel;
            WaveManager.StartPrepareState -= EnableUpgradesPanel;
            WaveManager.CompletedAllWaves -= EnableWinMenu;
            TurretHealth.PlayerDied -= EnableGameOverMenu;
            
            InputReaderSO.Unregister(EnablePauseMenu, inputReader.gameplayPause);
            InputReaderSO.Unregister(DisablePauseMenu, inputReader.pauseReturn);
        }

        private void EnableUpgradesPanel()
        {
            GlobalInputManager.DisableAllActionMapControls();
        
            upgradesPanel.SetActive(true);
        }

        private void DisableUpgradesPanel()
        {
            GlobalInputManager.InvokeChangeActionMapControls(GlobalInputManager.ActionMaps.Gameplay);
        
            upgradesPanel.SetActive(false);
        }

        private void EnablePauseMenu(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            
            GlobalInputManager.InvokeChangeActionMapControls(GlobalInputManager.ActionMaps.Pause);
            
            pauseMenu.SetActive(true);
        }

        private void DisablePauseMenu(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;
            
            DisablePauseMenu();
        }

        public void DisablePauseMenu()
        {
            GlobalInputManager.InvokeChangeActionMapControls(GlobalInputManager.ActionMaps.Gameplay);
            
            pauseMenu.SetActive(false);
        }

        public void EnableWinMenu()
        {
            GlobalInputManager.DisableAllActionMapControls();
            
            winMenu.SetActive(true);
        }
        
        public void EnableGameOverMenu()
        {
            GlobalInputManager.DisableAllActionMapControls();
            
            gameOverMenu.SetActive(true);
        }
        
        public void ReturnToMenu()
        {
            GlobalInputManager.InvokeChangeActionMapControls(GlobalInputManager.ActionMaps.Gameplay);
            
            SceneLoader.InvokeLoadMainMenuScene();
        }

        public void PlayAgain()
        {
            GlobalInputManager.InvokeChangeActionMapControls(GlobalInputManager.ActionMaps.Gameplay);
            
            playerData.Reset();
            
            SceneLoader.InvokeLoadGameplayScene();
        }
    }
}
