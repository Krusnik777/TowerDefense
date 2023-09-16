using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_episode;
        [SerializeField] private Image[] m_resultImages;
        [SerializeField] private GameObject m_confirmCanvasPrefab;
        [SerializeField] private Animator m_animator;

        private int ThisEpisodeScore() { return MapCompletion.Instance.GetEpisodeScore(m_episode); }

        private bool isComplete;
        public bool IsComplete => isComplete;

        public void ConfirmStartLevel()
        {
            var score = ThisEpisodeScore();

            if (score <= 0)
            {
                LoadLevel();
            }
            else
            {
                var confirmPanel = Instantiate(m_confirmCanvasPrefab);
                confirmPanel.GetComponent<ConfirmPanel>().SetParent(this);
            }
        }

        public void LoadLevel()
        {
            if (m_episode) LevelSequenceController.Instance.StartEpisode(m_episode);
        }

        public void Initialize()
        {
            var score = ThisEpisodeScore();

            isComplete = score > 0;

            for (int i = 0; i < score; i++)
            {
                m_resultImages[i].enabled = false;
            }

            if (m_animator != null && isComplete)
            {
                m_animator.enabled = false;
            }
        }

        public void Initialize(ref int score)
        {
            score = ThisEpisodeScore();

            isComplete = score > 0;

            for (int i = 0; i < score; i++)
            {
                m_resultImages[i].enabled = false;
            }

            if (m_animator != null && isComplete)
            {
                m_animator.enabled = false;
            }
        }

    }
}
