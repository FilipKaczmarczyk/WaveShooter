using TMPro;
using UnityEngine;
using Waves;

namespace UI
{
    public class UIWaveTrackerPanel : MonoBehaviour
    {
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private GameObject timeToNextWavePanel;
        [SerializeField] private GameObject enemiesLeftPanel;
        [SerializeField] private TextMeshProUGUI timeToNextWaveValueText;
        [SerializeField] private TextMeshProUGUI enemiesLeftValueText;
    
        private void OnEnable()
        {
            CheckWaveState();

            WaveManager.StartFightingState += SwitchToEnemiesLeftPanel;
            WaveManager.StartPrepareState += SwitchToTimeToNextWavePanel;
        }

        private void OnDisable()
        {
            WaveManager.StartFightingState -= SwitchToEnemiesLeftPanel;
            WaveManager.StartPrepareState -= SwitchToTimeToNextWavePanel;
        }

        private void CheckWaveState()
        {
            if (waveManager.GetCurrentWaveState() == WaveManager.WaveState.Prepare)
            {
                SwitchToTimeToNextWavePanel();
            }
            else
            {
                SwitchToEnemiesLeftPanel();
            }
        }
    
        private void SwitchToTimeToNextWavePanel()
        {
            timeToNextWavePanel.SetActive(true);
            enemiesLeftPanel.SetActive(false);
        }
    
        private void SwitchToEnemiesLeftPanel()
        {
            timeToNextWavePanel.SetActive(false);
            enemiesLeftPanel.SetActive(true);
        }

        public void UpdateTimeToNextWaveText(float value)
        {
            timeToNextWaveValueText.SetText(value.ToString("F1") + "s");
        }

        public void UpdateEnemiesCountText(int value)
        {
            enemiesLeftValueText.SetText(value.ToString());
        }
    }
}
