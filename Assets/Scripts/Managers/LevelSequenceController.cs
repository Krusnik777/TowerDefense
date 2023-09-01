using UnityEngine.SceneManagement;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        public static string LevelMapSceneNickname = "LevelMap";

        public Episode CurrentEpisode { get; private set; }

        public int CurrentLevel { get; private set; }

        public bool LastLevelResult { get; private set; }

        //public PlayerStatistics LevelStatistics { get; private set; }

        public static SpaceShip PlayerShip { get; set; }

        public void StartEpisode(Episode episode)
        {
            CurrentEpisode = episode;
            CurrentLevel = 0;

            //LevelStatistics = new PlayerStatistics();
            //LevelStatistics.Reset();

            SceneManager.LoadScene(episode.Levels[CurrentLevel]);
        }

        public void RestartLevel()
        {
            //SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void FinishCurrentLevel(bool state)
        {
            LastLevelResult = state;

            //CalculateLevelStatistics();

            ResultPanelController.Instance.Show(state);
        }

        public void AdvanceLevel()
        {
            //LevelStatistics.Reset();

            CurrentLevel++;

            if (CurrentEpisode.Levels.Length <= CurrentLevel)
            {
                SceneManager.LoadScene(LevelMapSceneNickname);
            }
            else
            {
                SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
            }
        }
        /*
        private void CalculateLevelStatistics()
        {
            LevelStatistics.Score = Player.Instance.Score;
            LevelStatistics.NumKills = Player.Instance.NumKills;
            LevelStatistics.Time = (int)LevelController.Instance.LevelTime;

            if (LevelController.Instance.LevelTime < LevelController.Instance.ReferenceTime && LevelController.Instance.LevelTime != 0.0f)
            {
                LevelStatistics.Score += (int)(LevelController.Instance.BonusScore * (LevelController.Instance.ReferenceTime/LevelController.Instance.LevelTime));
            }

            OverallStatistics.Instance.UpdateOverallStatistics(LevelStatistics.NumKills, LevelStatistics.Score, LevelStatistics.Time);
        }*/
    }
}
