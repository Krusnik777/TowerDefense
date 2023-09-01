using UnityEngine;

namespace SpaceShooter
{
    public class OverallStatistics : SingletonBase<OverallStatistics>
    {
        private int m_allKills;
        public int AllKills => m_allKills;

        private int m_allScore;
        public int AllScore => m_allScore;

        private int m_allTime;
        public int AllTime => m_allTime;

        protected override void Awake()
        {
            base.Awake();

            Load();
        }

        public void UpdateOverallStatistics(int kills, int score, int time)
        {
            m_allKills += kills;
            m_allScore += score;
            m_allTime += time;

            Save();
        }

        private void Save()
        {
            PlayerPrefs.SetInt("AllKills", m_allKills);
            PlayerPrefs.SetInt("AllScore", m_allScore);
            PlayerPrefs.SetInt("AllTime", m_allTime);
        }

        private void Load()
        {
            m_allKills = PlayerPrefs.GetInt("AllKills", 0);
            m_allScore = PlayerPrefs.GetInt("AllScore", 0);
            m_allTime = PlayerPrefs.GetInt("AllTime", 0);
        }

        public void Reset()
        {
            PlayerPrefs.DeleteAll();

            Load();
        }
    }
}
