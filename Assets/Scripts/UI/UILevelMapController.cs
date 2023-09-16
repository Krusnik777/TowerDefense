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
            Application.Quit();
        }

        private void OnMainMenuButton()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
        }

        private void OnUpgradeShipButton()
        {
            m_upgradeShopPanel.SetActive(!m_upgradeShopPanel.activeInHierarchy);
        }

    }
}
