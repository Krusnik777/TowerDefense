using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UILevelMapController : MonoBehaviour
    {
        [SerializeField] private Button m_exitButton;
        [SerializeField] private Button m_mainMenuButton;
        [SerializeField] private Button m_upgradeShopButton;
        [SerializeField] private GameObject m_upgradeShopPanel;

        private void Start()
        {
            m_upgradeShopPanel.SetActive(false);

            m_exitButton.onClick.AddListener(OnExitButton);
            m_mainMenuButton.onClick.AddListener(OnMainMenuButton);
            m_upgradeShopButton.onClick.AddListener(OnUpgradeShipButton);
        }

        private void OnDestroy()
        {
            m_exitButton.onClick.RemoveListener(OnExitButton);
            m_mainMenuButton.onClick.RemoveListener(OnMainMenuButton);
            m_upgradeShopButton.onClick.RemoveListener(OnUpgradeShipButton);
        }

        private void OnExitButton()
        {
            SceneController.Instance.QuitGame();
        }

        private void OnMainMenuButton()
        {
            SceneController.Instance.LoadMainMenu();
        }

        private void OnUpgradeShipButton()
        {
            m_upgradeShopPanel.SetActive(!m_upgradeShopPanel.activeInHierarchy);

            if (m_upgradeShopPanel.activeInHierarchy)
            {
                m_upgradeShopButton.GetComponentInChildren<Text>().text = "Close";
            }
            else
            {
                m_upgradeShopButton.GetComponentInChildren<Text>().text = "Upgrades";
            }
        }

    }
}
