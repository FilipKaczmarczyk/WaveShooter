using System;
using TMPro;
using UnityEngine;
using Waves;

namespace UI
{
    public class UIWaveCounterPanel : MonoBehaviour
    {
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private TextMeshProUGUI currentWaveText;
        
        private void OnEnable()
        {
            SetCurrentWaveText();

            WaveManager.StartFightingState += SetCurrentWaveText;
        }

        private void OnDisable()
        {
            WaveManager.StartFightingState -= SetCurrentWaveText;
        }

        private void SetCurrentWaveText()
        {
            var currentWave = waveManager.GetCurrentWave() + 1;
            
            currentWaveText.SetText("Wave " + currentWave + "/" + waveManager.GetWavesCount());
        }
    }
}
