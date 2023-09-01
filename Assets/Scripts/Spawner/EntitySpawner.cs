using UnityEngine;

namespace SpaceShooter
{

    public class EntitySpawner : Spawner
    {
        [SerializeField] private GameObject[] m_entityPrefabs;

        protected override GameObject GenerateSpawnedEntity()
        {
            int index = Random.Range(0, m_entityPrefabs.Length);

            GameObject e = Instantiate(m_entityPrefabs[index].gameObject);

            return e;
        }
    }
}
