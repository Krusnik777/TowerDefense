using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private Text m_moneyText;
        [SerializeField] private BuyUpgrade[] m_upgrades;

        private int m_money;

        private void Start()
        {
            foreach (var slot in m_upgrades)
            {
                slot.Initialize();

                var buttons = slot.GetComponentsInChildren<Button>();

                foreach (var button in buttons)
                {
                    button.onClick.AddListener(UpdateMoney);
                }
            }
            UpdateMoney();
        }

        private void OnDestroy()
        {
            foreach (var slot in m_upgrades)
            {
                var buttons = slot.GetComponentsInChildren<Button>();

                foreach (var button in buttons)
                {
                    button.onClick.RemoveListener(UpdateMoney);
                }
            }
        }

        public void UpdateMoney()
        {
            m_money = MapCompletion.Instance.TotalScore;
            m_money -= Upgrades.GetTotalCost();
            m_moneyText.text = m_money.ToString();
            foreach (var slot in m_upgrades)
            {
                slot.CheckCost(m_money);
            }
        }
    }
}
