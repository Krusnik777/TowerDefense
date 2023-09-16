using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{ 
    public class BuyUpgrade : MonoBehaviour
    {
        [SerializeField] private UpgradeAsset m_asset;
        [SerializeField] private Image m_upgradeIcon;
        [SerializeField] private Text m_nameText;
        [SerializeField] private Text m_levelText;
        [Header("Button")]
        [SerializeField] private Button m_iconButton;
        [SerializeField] private Button m_buyButton;
        [SerializeField] private Text m_buttonText;
        [SerializeField] private Image m_currencyImage;
        [SerializeField] private Text m_costText;
        [SerializeField] private Sound m_buySound = Sound.Shop;

        private int cost;
        public int Cost => cost;

        private void Start()
        {
            //m_buyButton.onClick.AddListener(Buy);
        }

        private void OnDestroy()
        {
            //m_buyButton.onClick.RemoveListener(Buy);
        }

        public void Initialize()
        {
            m_upgradeIcon.sprite = m_asset.Sprite;
            m_nameText.text = m_asset.Name;
            var savedLevel = Upgrades.GetUpgradeLevel(m_asset);

            if (savedLevel >= m_asset.CostByLevel.Length)
            {
                m_levelText.text = "Lv: Max";
                m_buyButton.interactable = false;
                m_iconButton.interactable = false;
                m_currencyImage.gameObject.SetActive(false);
                m_buttonText.text = "Maxed Out";
                m_costText.gameObject.SetActive(false);
                cost = int.MaxValue;
            }
            else
            {
                cost = m_asset.CostByLevel[savedLevel];
                m_levelText.text = "Lv: " + (savedLevel + 1).ToString();
                m_costText.text = cost.ToString();
            }
        }

        public void CheckCost(int money)
        {
            m_buyButton.interactable = money >= cost;
            m_iconButton.interactable = money >= cost;
        }

        public void Buy()
        {
            m_buySound.Play();
            Upgrades.BuyUpgrade(m_asset);
            Initialize();
        }
    }
}
