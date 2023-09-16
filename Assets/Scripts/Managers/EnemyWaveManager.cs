using System;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWaveManager : MonoBehaviour
    {
        public static event Action<Enemy> EventOnEnemySpawn;
        [SerializeField] private Enemy m_enemyPrefab;
        [SerializeField] private Path[] m_paths;
        [SerializeField] private EnemyWave m_currentWave;

        public event Action EventOnAllWavesDefeated;

        public event Action EventOnFinalWave;
        
        private int activeEnemyCount = 0;

        private void Start()
        {
            m_currentWave.Prepare(SpawnEnemies);
        }

        private void SpawnEnemies()
        {
            foreach((EnemyAsset asset, int count, int pathIndex) in m_currentWave.EnumerateSquads())
            {
                if (pathIndex < m_paths.Length)
                {
                    for (int i = 0; i < count; i++)
                    {
                        var e = Instantiate(m_enemyPrefab, m_paths[pathIndex].StartArea.GetRandomInsideZone(), Quaternion.identity);
                        e.GetComponent<SpaceShooter.Destructible>().EventOnDeath.AddListener(RecordEnemyDead);
                        e.GetComponent<TDPatrolController>().EventOnEndPath.AddListener(RecordEnemyDead);
                        e.Use(asset);
                        e.GetComponent<TDPatrolController>().SetPath(m_paths[pathIndex]);

                        activeEnemyCount++;

                        EventOnEnemySpawn?.Invoke(e);
                    }
                }
                else
                {
                    Debug.LogWarning($"Invalid pathIndex int {name}");
                }
            }
            m_currentWave = m_currentWave.PrepareNext(SpawnEnemies);

            if (m_currentWave == null) EventOnFinalWave?.Invoke();
        }

        private void RecordEnemyDead()
        {
            if (--activeEnemyCount == 0) ForceNextWave();
        }

        public void ForceNextWave()
        {
            if (m_currentWave)
            {
                TDPlayer.Instance.ChangeGold((int)m_currentWave.GetRemainingTime());
                SpawnEnemies();
            }
            else
            {
                if (activeEnemyCount == 0) EventOnAllWavesDefeated?.Invoke();
            }
        }
    }
}
