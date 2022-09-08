using System;
using TMPro;
using Turret;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class UIHealthPointsPanel : MonoBehaviour
    {
        [SerializeField] private TurretHealth turretHealth;
        [SerializeField] private Slider healthSlider;
        [SerializeField] private TextMeshProUGUI healthText;
        
        private void OnEnable()
        {
            UpdateHealthUI();

            TurretHealth.TurretHealthChange += UpdateHealthUI;
        }

        private void OnDisable()
        {
            TurretHealth.TurretHealthChange += UpdateHealthUI;
        }

        private void UpdateHealthUI()
        {
            healthText.SetText("HP " + turretHealth.CurrentHealth + "/" + turretHealth.GetMaxHealth());

            var healthPercentValue = (float)turretHealth.CurrentHealth / turretHealth.GetMaxHealth();

            healthSlider.value = healthPercentValue;
        }
    }
}
