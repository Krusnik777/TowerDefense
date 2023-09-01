using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TowerDefense
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private Button m_newGameButton;
        [SerializeField] private Button m_continueButton;
        [SerializeField] private Button m_quitButton;
        [Header("NewGameStartConfirmPanel")]
        [SerializeField] private GameObject m_confirmPanel;
        [SerializeField] private Button m_confirmButton;
        [SerializeField] private Button m_declineButton;

        private void Start()
        {
            m_newGameButton.onClick.AddListener(ConfirmNewGame);
            m_continueButton.onClick.AddListener(Continue);
            m_quitButton.onClick.AddListener(Quit);

            m_confirmButton.onClick.AddListener(NewGame);
            m_declineButton.onClick.AddListener(CloseConfirmPanel);

            m_continueButton.interactable = FileHandler.TryGetFile(MapCompletion.Filename);

            CloseConfirmPanel();
        }
        private void OnDestroy()
        {
            m_newGameButton.onClick.RemoveListener(ConfirmNewGame);
            m_continueButton.onClick.RemoveListener(Continue);
            m_quitButton.onClick.RemoveListener(Quit);

            m_confirmButton.onClick.RemoveListener(NewGame);
            m_declineButton.onClick.RemoveListener(CloseConfirmPanel);
        }

        public void ConfirmNewGame()
        {
            if (FileHandler.TryGetFile(MapCompletion.Filename))
            {
                m_confirmPanel.SetActive(true);
            }
            else
            {
                NewGame();
            }
        }

        private void NewGame()
        {
            FileHandler.Reset(MapCompletion.Filename);
            if (FileHandler.TryGetFile(Upgrades.Filename)) FileHandler.Reset(Upgrades.Filename);
            SceneManager.LoadScene(1);
        }
        private void Continue()
        {
            SceneManager.LoadScene(1);
        }
        private void Quit()
        {
            Application.Quit();
        }

        private  void CloseConfirmPanel()
        {
            m_confirmPanel.SetActive(false);
        }
        
    }
}
