using UnityEngine;
using SpaceShooter;
using System;

namespace TowerDefense
{
    public class MapCompletion : SingletonBase<MapCompletion>
    {
        public const string Filename = "Completion.dat";

        [Serializable]
        private class EpisodeScore
        {
            public Episode Episode;
            public int Score;
        }

        [SerializeField] private EpisodeScore[] m_completionData;

        private int totalScore;
        public int TotalScore => totalScore;

        private new void Awake()
        {
            base.Awake();
            Saver<EpisodeScore[]>.TryLoad(Filename, ref m_completionData);
            UpdateTotalScore();
        }

        private void UpdateTotalScore()
        {
            totalScore = 0;

            foreach (var episodeScore in m_completionData)
            {
                totalScore += episodeScore.Score;
            }
        }

        public static void SaveEpisodeResult(int levelScore)
        {
            if (Instance)
                Instance.SaveResult(LevelSequenceController.Instance.CurrentEpisode, levelScore);
            else
                Debug.Log($"Episode complete with score {levelScore}");
        }

        private void SaveResult(Episode currentEpisode, int levelScore)
        {
            foreach (var item in m_completionData)
            {
                if (item.Episode == currentEpisode)
                {
                    if (levelScore > item.Score)
                    {
                        item.Score = levelScore;
                        Saver<EpisodeScore[]>.Save(Filename, m_completionData);
                        UpdateTotalScore();
                    }
                }
            }
        }

        public int GetEpisodeScore(Episode episode)
        {
            foreach(var data in m_completionData)
            {
                if (data.Episode == episode) return data.Score;
            }
            return 0;
        }
    }
}
