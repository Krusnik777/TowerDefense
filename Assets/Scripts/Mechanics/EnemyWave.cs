using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefense
{
    public class EnemyWave : MonoBehaviour
    {
        [Serializable]
        private class Squad
        {
            public EnemyAsset Asset;
            public int Count;
        }

        [Serializable]
        private class PathGroup
        {
            public Squad[] Squads;
        }

        [SerializeField] private PathGroup[] m_groups;
        [SerializeField] private float m_prepareTime = 10f;
        [SerializeField] private EnemyWave m_nextWave;

        public static Action<float> EventOnWavePrepare;

        private event Action eventOnWaveReady;

        public float GetRemainingTime() { return m_prepareTime - Time.time; }

        private void Awake()
        {
            enabled = false;
        }

        private void Update()
        {
            if (Time.time >= m_prepareTime)
            {
                enabled = false;
                eventOnWaveReady?.Invoke();
            }
        }

        public void Prepare(Action spawnEnemies)
        {
            EventOnWavePrepare?.Invoke(m_prepareTime);
            m_prepareTime += Time.time;
            enabled = true;
            eventOnWaveReady += spawnEnemies;
        }

        public IEnumerable<(EnemyAsset asset, int count, int pathIndex)> EnumerateSquads()
        {
            for (int i = 0; i< m_groups.Length; i++)
            {
                foreach (var squad in m_groups[i].Squads)
                {
                    yield return (squad.Asset, squad.Count, i);
                }
            }
        }

        public EnemyWave PrepareNext(Action spawnEnemies)
        {
            eventOnWaveReady -= spawnEnemies;
            if (m_nextWave) m_nextWave.Prepare(spawnEnemies);
            return m_nextWave;
        }
    }
}