using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    public class BriefingGUI : MonoBehaviour
    {
        [SerializeField] private Text m_headerText;
        [SerializeField] private Text m_messageText;
        [SerializeField] private Button m_returnButton;
        [SerializeField] private Button m_advanceButton;

        private void Start()
        {
            m_returnButton.onClick.AddListener(OnReturnButton);
            m_advanceButton.onClick.AddListener(OnAdvanceButton);

            m_headerText.text = LevelSequenceController.Instance.CurrentEpisode.EpisodeName;
            m_messageText.text = LevelSequenceController.Instance.CurrentEpisode.BriefingText;
        }

        private void OnDestroy()
        {
            m_returnButton.onClick.RemoveListener(OnReturnButton);
            m_advanceButton.onClick.RemoveListener(OnAdvanceButton);
        }

        private void OnReturnButton()
        {
            LevelSequenceController.Instance.ReturnToLevelMap();
        }    

        private void OnAdvanceButton()
        {
            LevelSequenceController.Instance.AdvanceLevel();
        }
    }
}
