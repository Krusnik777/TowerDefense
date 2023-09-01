using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(AudioSource))]
    public class Turret : MonoBehaviour
    {
        [SerializeField] private TurretMode m_mode;
        public TurretMode Mode => m_mode;

        [SerializeField] private TurretProperties m_turretProperties;

        private float m_refireTimer;

        public bool CanFire => m_refireTimer <= 0;

        private SpaceShip m_ship;

        private AudioSource m_audioSource;

        #region UnityEvents

        private void Start()
        {
            m_ship = transform.root.GetComponent<SpaceShip>();
            m_audioSource = GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (m_refireTimer > 0)
            {
                m_refireTimer -= Time.deltaTime;
            }
            else if (m_mode == TurretMode.Auto)
            {
                Fire();
            }
        }

        #endregion

        #region PublicAPI

        public void Fire()
        {
            if (m_turretProperties == null) return;

            if (m_refireTimer > 0) return;

            if (m_ship)
            {
                if (m_ship.DrawEnergy(m_turretProperties.EnergyUsage) == false) return;

                if (m_ship.DrawAmmo(m_turretProperties.AmmoUsage) == false) return;
            }

            Projectile projectile = Instantiate(m_turretProperties.ProjectilePrefab).GetComponent<Projectile>();
            projectile.transform.position = transform.position;
            projectile.transform.up = transform.up;

            projectile.SetParentShooter(m_ship);

            m_refireTimer = m_turretProperties.RateOfFire;

            if (m_turretProperties.LaunchSFX != null)
            {
                m_audioSource.clip = m_turretProperties.LaunchSFX;
                m_audioSource.Play();
            }

        }

        public void AssignLoadout(TurretProperties props)
        {
            if (m_mode != props.Mode) return;

            m_refireTimer = 0;
            m_turretProperties = props;
        }

        #endregion
    }
}
