using UnityEngine.SceneManagement;
using TowerDefense;

namespace SpaceShooter
{
    public class LevelSequenceController : SingletonBase<LevelSequenceController>
    {
        //public static string LevelMapSceneNickname = "LevelMap";

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

            SceneController.Instance.LoadSceneWithFade(episode.Levels[CurrentLevel]);
            //SceneManager.LoadScene(episode.Levels[CurrentLevel]);
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
                ReturnToLevelMap();
            }
            else
            {
                if (CurrentEpisode.HasFinalScene && CurrentLevel + 1 >= CurrentEpisode.Levels.Length && TowerDefense.MapCompletion.Instance.SeenFinalScene)
                {
                    ReturnToLevelMap();
                }
                else
                {
                    SceneController.Instance.LoadSceneWithFade(CurrentEpisode.Levels[CurrentLevel]);
                    //SceneManager.LoadScene(CurrentEpisode.Levels[CurrentLevel]);
                } 
            }
        }

        public void ReturnToLevelMap()
        {
            SceneController.Instance.LoadLevelMap();
            //SceneManager.LoadScene(LevelMapSceneNickname);
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
