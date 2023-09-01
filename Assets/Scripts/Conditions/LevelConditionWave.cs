using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class LevelConditionWave : MonoBehaviour, ILevelCondition
    {
        private bool isCompleted;

        private void Start()
        {
            FindObjectOfType<EnemyWaveManager>().EventOnAllWavesDefeated += () =>
            {
                isCompleted = true;
            };
        }

        public bool IsCompleted
        {
            get 
            {
                return isCompleted;
            }
        }
    }
}
