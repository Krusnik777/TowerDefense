using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{
    public class EnemySpawner : Spawner
    {
        [SerializeField] private Enemy m_enemyPrefab;
        [SerializeField] private EnemyAsset[] m_enemyAssets;
        [SerializeField] private Path m_patrolPath;

        protected override GameObject GenerateSpawnedEntity()
        {
            var e = Instantiate(m_enemyPrefab);

            e.Use(m_enemyAssets[Random.Range(0, m_enemyAssets.Length)]);

            e.GetComponent<TDPatrolController>().SetPath(m_patrolPath);

            return e.gameObject;
        }
    }
}
