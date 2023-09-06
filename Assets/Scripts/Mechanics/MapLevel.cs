using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    public class MapLevel : MonoBehaviour
    {
        [SerializeField] private Episode m_episode;
        [SerializeField] private Image[] m_resultImages;

        private bool isComplete;
        public bool IsComplete => isComplete;

        public void LoadLevel()
        {
            if (m_episode) LevelSequenceController.Instance.StartEpisode(m_episode);
        }

        public void Initialize()
        {
            var score = MapCompletion.Instance.GetEpisodeScore(m_episode);

            isComplete = score > 0;

            for (int i = 0; i < score; i++)
            {
                m_resultImages[i].enabled = false;
            }
        }

        public void Initialize(ref int score)
        {
            score = MapCompletion.Instance.GetEpisodeScore(m_episode);

            isComplete = score > 0;

            for (int i = 0; i < score; i++)
            {
                m_resultImages[i].enabled = false;
            }
        }
    }
}
