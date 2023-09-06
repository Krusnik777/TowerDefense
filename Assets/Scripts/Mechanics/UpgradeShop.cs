using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UpgradeShop : MonoBehaviour
    {
        [SerializeField] private int m_money;
        [SerializeField] private Text m_moneyText;
        [SerializeField] private BuyUpgrade[] m_upgrades;

        private void Start()
        {
            foreach (var slot in m_upgrades)
            {
                slot.Initialize();
                slot.GetComponentInChildren<Button>().onClick.AddListener(UpdateMoney);
            }
            UpdateMoney();
        }

        private void OnDestroy()
        {
            foreach (var slot in m_upgrades)
            {
                slot.GetComponentInChildren<Button>().onClick.RemoveListener(UpdateMoney);
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
