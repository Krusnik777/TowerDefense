using System;
using System.Collections;
using UnityEngine;

namespace SpaceShooter
{
    public class Player : SingletonBase<Player>
    {
        [SerializeField] private int m_numLives;
        [SerializeField] private SpaceShip m_ship;
        //[SerializeField] private CameraController m_cameraController;
        //[SerializeField] private MovementController m_movementController;
        //[SerializeField] private MinimapController m_minimapController;
        public int Lives => m_numLives;
        public SpaceShip ActiveShip => m_ship;

        public event Action EventOnPlayerDead;

        protected override void Awake()
        {
            base.Awake();

            if (m_ship != null)
                Destroy(m_ship.gameObject);
        }

        private void Start()
        {
            Respawn();
        }

        private void OnShipDeath()
        {
            m_numLives--;
            StartCoroutine(RespawnCoroutine());
        }

        private IEnumerator RespawnCoroutine()
        {
            yield return new WaitForSeconds(1);

            if (m_numLives >= 0)
                Respawn();
            else
                LevelSequenceController.Instance.FinishCurrentLevel(false);
        }

        protected void TakeDamage(int damage)
        {
            m_numLives -= damage;
            if (m_numLives <= 0)
            {
                m_numLives = 0;
                EventOnPlayerDead?.Invoke();
            }
        }

        private void Respawn()
        {
            if (LevelSequenceController.PlayerShip != null)
            {
                var newPlayerShip = Instantiate(LevelSequenceController.PlayerShip.gameObject);

                m_ship = newPlayerShip.GetComponent<SpaceShip>();

                //m_cameraController.SetTarget(m_ship.transform);
                //m_movementController.SetTargetShip(m_ship);
                //m_minimapController.SetTarget(m_ship.transform);

                m_ship.EventOnDeath.AddListener(OnShipDeath);
            }
        }

        public void ImmediateDeath()
        {
            m_numLives = 0;
            OnShipDeath();
        }

        #region Score

        public int Score { get; private set; }
        public int NumKills { get; private set; }

        public void AddKill()
        {
            NumKills++;
        }

        public void AddScore(int num)
        {
            Score += num;
        }

        #endregion

        
    }
}
