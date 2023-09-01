using UnityEngine;
using UnityEngine.Events;

namespace SpaceShooter
{
    [RequireComponent(typeof(SpaceShip), typeof(AIController))]
    public class Boss : MonoBehaviour
    {
        [SerializeField] private int m_numPhases = 1;
        [SerializeField] private int m_breakPoints;
        [SerializeField] private bool m_unbreakable;
        [SerializeField] private float m_damageBooster;
        [SerializeField] private float m_breakStateTime;
        [SerializeField] private UnityEvent m_eventOnBreak;
        [SerializeField] private UnityEvent[] m_eventOnPhaseEnd;
        [SerializeField] private GameObject m_breakEffectPrefab;
        [SerializeField] private GameObject m_phaseEndEffectPrefab;

        public float DamageBooster => m_damageBooster;

        private bool m_inBreakState;
        public bool InBreakState => m_inBreakState;

        private int m_currentBreakPoints;
        public int BreakPoints => m_currentBreakPoints;

        private int m_HPforPhase;

        private int m_currentPhase;

        private int[] m_savedHP;

        private SpaceShip m_bossShipBody;

        private float timer;

        private void Start()
        {
            m_currentBreakPoints = m_breakPoints;

            timer = 0;

            m_bossShipBody = GetComponent<SpaceShip>();

            if (m_numPhases > 1)
            {
                m_HPforPhase = m_bossShipBody.MaxHitPoints / m_numPhases;

                m_savedHP = new int[m_numPhases-1];
                for (int i = 0; i < m_savedHP.Length; i++)
                {
                    m_savedHP[i] = m_bossShipBody.MaxHitPoints - (m_HPforPhase *(i+1));
                }

                m_currentPhase = 1;
            }
        }

        private void Update()
        {
            CheckPhaseEnd();

            if (m_inBreakState)
            {
                timer += Time.deltaTime;

                if(timer >= m_breakStateTime)
                {
                    ResetBreakState();
                }
            }
        }

        public void ApplyBreakDamage(object sender, int damage)
        {
            if (m_unbreakable || m_inBreakState) return;

            m_currentBreakPoints -= damage;

            if (m_currentBreakPoints <= 0)
            {
                OnBreak();
            }
        }

        private void OnBreak()
        {
            m_eventOnBreak?.Invoke();

            if (m_breakEffectPrefab != null)
            {
                var breakEffect = Instantiate(m_breakEffectPrefab, transform.position, Quaternion.identity);

                Destroy(breakEffect, 1);
            }

            m_inBreakState = true;
        }

        private void CheckPhaseEnd()
        {
            if (m_numPhases <= 1 || m_currentPhase >= m_numPhases) return;

            if (m_bossShipBody.HitPoints <= m_savedHP[m_currentPhase - 1])
            {
                OnPhaseEnd(m_currentPhase - 1);

                m_currentPhase++;

            }

        }

        private void OnPhaseEnd(int index)
        {
            m_eventOnPhaseEnd[index]?.Invoke();

            ResetBreakState();

            if (m_phaseEndEffectPrefab != null)
            {
                var phaseChangeEffect = Instantiate(m_phaseEndEffectPrefab, transform.position, Quaternion.identity);

                Destroy(phaseChangeEffect, 1);
            }

        }

        private void ResetBreakState()
        {
            m_inBreakState = false;

            m_currentBreakPoints = m_breakPoints;

            timer = 0;
        }

#if UNITY_EDITOR

        private void OnValidate()
        {
            if (m_numPhases < 1) m_numPhases = 1;

            if (m_numPhases > 1) m_eventOnPhaseEnd = new UnityEvent[m_numPhases-1];
        }

#endif

    }
}
