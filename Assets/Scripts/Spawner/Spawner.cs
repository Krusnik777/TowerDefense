using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(CircleArea))]
    public abstract class Spawner : MonoBehaviour
    {
        [SerializeField] private CircleArea m_area;
        public enum SpawnMode
        {
            Start,
            Loop
        }
        [SerializeField] private SpawnMode m_spawnMode;
        [SerializeField] private int m_numSpawns;
        [SerializeField] private bool m_spawnAtStart;
        [SerializeField] private float m_respawnTime;

        private float timer;

        protected abstract GameObject GenerateSpawnedEntity();

        private void Start()
        {
            if (m_spawnMode == SpawnMode.Start || m_spawnAtStart)
            {
                SpawnEntities();
            }

            timer = m_respawnTime;
        }

        private void Update()
        {
            if (timer > 0)
                timer -= Time.deltaTime;

            if (m_spawnMode == SpawnMode.Loop && timer <= 0)
            {
                SpawnEntities();

                timer = m_respawnTime;
            }
        }

        private void SpawnEntities()
        {
            for (int i = 0; i < m_numSpawns; i++)
            {
                var e = GenerateSpawnedEntity();
                e.transform.position = m_area.GetRandomInsideZone();
            }
        }
    }
}
