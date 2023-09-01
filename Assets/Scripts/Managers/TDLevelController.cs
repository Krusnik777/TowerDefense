using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDLevelController : LevelController
    {
        private int levelScore = 3;
        public int LevelScore => levelScore;

        public static new TDLevelController Instance { get { return LevelController.Instance as TDLevelController; } }

        private new void Start()
        {
            base.Start();
            TDPlayer.Instance.EventOnPlayerDead += () =>
            {
                m_isLevelCompleted = true;
                levelScore = 0;
                StopLevelActivity();
                ResultPanelController.Instance.Show(false);
            };

            m_referenceTime += Time.time;

            m_eventLevelCompleted.AddListener(() =>
            {
                StopLevelActivity();
                if (m_referenceTime <= Time.time)
                {
                    levelScore--;
                }
                MapCompletion.SaveEpisodeResult(levelScore);
            });

            void LifeScoreChange(int _)
            {
                levelScore--;
                TDPlayer.EventOnLifeUpdate -= LifeScoreChange;
            }

            TDPlayer.EventOnLifeUpdate += LifeScoreChange;
        }

        private void StopLevelActivity()
        {
            foreach(var dest in Destructible.AllDestructibles)
            {
                if (dest.TryGetComponent(out Enemy enemy))
                {
                    enemy.GetComponent<SpaceShip>().enabled = false;
                    enemy.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
                }
            }

            void DisableAll<T>() where T : MonoBehaviour
            {
                foreach (var obj in FindObjectsOfType<T>())
                {
                    obj.enabled = false;
                }
            }

            DisableAll<Spawner>();
            DisableAll<Projectile>();
            DisableAll<Tower>();
            DisableAll<NextWaveGUI>();
        }
    }
}
