using Input;
using UnityEngine;
using Waves;

namespace UI
{
    public class UICanvasController : MonoBehaviour
    {
        [SerializeField] private GameObject upgradesPanel;
    
        private void OnEnable()
        {
            WaveManager.StartFightingState += DisableUpgradesPanel;
            WaveManager.StartPrepareState += EnableUpgradesPanel;
        }

        private void OnDisable()
        {
            WaveManager.StartFightingState -= DisableUpgradesPanel;
            WaveManager.StartPrepareState -= EnableUpgradesPanel;
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
    
    }
}
