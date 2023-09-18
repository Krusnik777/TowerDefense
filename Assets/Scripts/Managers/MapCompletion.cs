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

        [Serializable]
        private class CompletionData
        {
            public EpisodeScore[] EpisodeScores;
            public bool CompletedMainStory;
        }

        [SerializeField] private CompletionData m_completionData;

        public bool SeenFinalScene => m_completionData.CompletedMainStory;

        private int totalScore;
        public int TotalScore => totalScore;

        private new void Awake()
        {
            base.Awake();
            Saver<CompletionData>.TryLoad(Filename, ref m_completionData);
            UpdateTotalScore();
        }

        private void UpdateTotalScore()
        {
            totalScore = 0;

            foreach(var episodeScore in m_completionData.EpisodeScores)
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
            foreach (var item in m_completionData.EpisodeScores)
            {
                if (item.Episode == currentEpisode)
                {
                    if (levelScore > item.Score)
                    {
                        item.Score = levelScore;
                        Saver<CompletionData>.Save(Filename, m_completionData);
                        UpdateTotalScore();
                    }
                }
            }
        }

        public int GetEpisodeScore(Episode episode)
        {
            foreach(var data in m_completionData.EpisodeScores)
            {
                if (data.Episode == episode) return data.Score;
            }

            return 0;
        }

        public void SaveSeenFinalScene()
        {
            m_completionData.CompletedMainStory = true;
            Saver<CompletionData>.Save(Filename, m_completionData);
        }
    }
}
