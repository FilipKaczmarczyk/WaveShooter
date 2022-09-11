using System;
using System.Text;
using Events;
using PlayerData;
using PlayerData.ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Upgrades.ScriptableObjects;

namespace UI
{
    public class UIUpgradePanel : MonoBehaviour
    {
        private enum UpgradePanelType
        {
            Gun,
            Ammo,
            AutoTurret
        }

        public static event EventManager.VoidEventHandler GunUpgrade;
        public static event EventManager.VoidEventHandler RefreshPanels;
    
        [Header("UI references")] 
        [SerializeField] private TextMeshProUGUI upgradeNameText;
        [SerializeField] private TextMeshProUGUI upgradeCostText;
        [SerializeField] private Image upgradeImage;
        [SerializeField] private GameObject notEnoughMoney;
        [SerializeField] private GameObject fullyUpgraded;
    
        [Header("Upgrade references")]
        [SerializeField] private PlayerDataSO playerData;
        [SerializeField] private UpgradePanelType upgradePanelType;

        private UpgradeSO _nextUpgrade;
    
        private void OnEnable()
        {
            RefreshPanels += RefreshPanel;
        }

        private void OnDisable()
        {
            RefreshPanels -= RefreshPanel;
        }
    
        private void Awake()
        {
            RefreshPanel();
        }
    
        private void RefreshPanel()
        {
            if (GetNextUpgrade() != null)
            {
                CreateNextUpgradePanel();
            }
            else
            {
                SetFullyUpgradedImage();
            }
        }

        private UpgradeSO GetNextUpgrade()
        {
            return upgradePanelType switch
            {
                UpgradePanelType.Ammo => playerData.CurrentSelectedAmmo.nextUpgrade,
                UpgradePanelType.Gun => playerData.CurrentSelectedGun.nextUpgrade,
                UpgradePanelType.AutoTurret => playerData.AutoTurretCount < playerData.maxTurretCount ? playerData.autoTurret : null,
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    
        private void CreateNextUpgradePanel()
        {
            _nextUpgrade = GetNextUpgrade();

            upgradeNameText.text = _nextUpgrade.upgradeName;

            var nextUpgradeCostText = new StringBuilder();
        
            nextUpgradeCostText
                .Append("(Cost: ")
                .Append(_nextUpgrade.upgradeCost.ToString())
                .Append("$)");

            upgradeCostText.text = nextUpgradeCostText.ToString();
            upgradeImage.sprite = _nextUpgrade.upgradeImage;

            SetNotEnoughMoneyImage(!IsEnoughMoney(_nextUpgrade.upgradeCost));
        }

        private bool IsEnoughMoney(int moneyNeeded)
        {
            return PlayerMoneyHandler.CurrentPlayerMoney >= moneyNeeded;
        }

        private void SetNotEnoughMoneyImage(bool state)
        {
            notEnoughMoney.SetActive(state);
        }

        private void SetFullyUpgradedImage()
        {
            fullyUpgraded.SetActive(true);
        }

        public void BuyUpgrade()
        {
            switch (upgradePanelType)
            {
                case UpgradePanelType.Ammo:
                    playerData.CurrentSelectedAmmo = playerData.CurrentSelectedAmmo.nextUpgrade;
                    break;
            
                case UpgradePanelType.Gun:
                    playerData.CurrentSelectedGun = playerData.CurrentSelectedGun.nextUpgrade;
                    GunUpgrade?.Invoke();
                    break;
            }

            PlayerMoneyHandler.DecreaseMoney(_nextUpgrade.upgradeCost);

            RefreshPanels?.Invoke();
        
            RefreshPanel();
        }
    }
}
