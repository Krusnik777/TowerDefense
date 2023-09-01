using System;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class EnemyWaveController : MonoBehaviour
    {
        [Serializable]
        public class EnWave
        {
            public GameObject ObjectWithSpawners;
            public float DurationTime;
        }
        
        [SerializeField] private EnWave[] m_enemyWaves;
        [SerializeField] private Button m_startWaveButton;

        private float timer;

        private int currentWave;

        private bool waveIsActive;

        private void Awake()
        {
            foreach(var wave in m_enemyWaves)
            {
                wave.ObjectWithSpawners.SetActive(false);
            }

            currentWave = 0;

            waveIsActive = false;

            m_startWaveButton.onClick.AddListener(StartWave);
        }

        public void StartWave()
        {
            if (currentWave >= m_enemyWaves.Length) return;

            m_enemyWaves[currentWave].ObjectWithSpawners.SetActive(true);
            timer = m_enemyWaves[currentWave].DurationTime;

            m_startWaveButton.gameObject.SetActive(false);

            waveIsActive = true;
        }

        private void Update()
        {
            if (waveIsActive)
            {
                timer -= Time.deltaTime;

                if (timer <= 0)
                {
                    m_enemyWaves[currentWave].ObjectWithSpawners.SetActive(false);

                    waveIsActive = false;

                    currentWave++;

                    if (currentWave < m_enemyWaves.Length) m_startWaveButton.gameObject.SetActive(true);
                }
            }
        }

        private void OnDestroy()
        {
            m_startWaveButton.onClick.RemoveListener(StartWave);
        }


    }
}
