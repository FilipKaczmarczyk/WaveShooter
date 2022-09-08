using System;
using TMPro;
using UnityEngine;
using Waves;

namespace UI
{
    public class UICurrentWavePanel : MonoBehaviour
    {
        [SerializeField] private WaveManager waveManager;
        [SerializeField] private TextMeshProUGUI currentWaveText;
        
        private void OnEnable()
        {
            SetCurrentWaveText();

            WaveManager.WaveChange += SetCurrentWaveText;
        }

        private void OnDisable()
        {
            WaveManager.WaveChange -= SetCurrentWaveText;
        }

        private void SetCurrentWaveText()
        {
            var currentWave = waveManager.GetCurrentWave() + 1;
            
            currentWaveText.SetText("Wave " + currentWave + "/" + waveManager.GetWavesCount());
        }
    }
}
