using PlayerData;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UIMoneyPanel : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI moneyValueText;
        
        private void OnEnable()
        {
            PlayerMoneyHandler.RefreshMoney += RefreshMoneyUI;
        }

        private void OnDisable()
        {
            PlayerMoneyHandler.RefreshMoney -= RefreshMoneyUI;
        }

        private void RefreshMoneyUI()
        {
            moneyValueText.SetText(PlayerMoneyHandler.CurrentPlayerMoney.ToString());
        }
    }
}
