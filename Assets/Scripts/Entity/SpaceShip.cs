using TowerDefense;
using UnityEngine;

namespace SpaceShooter
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class SpaceShip : Destructible
    {
        /// <summary>
        /// Mass for rigid
        /// </summary>
        [Header("Space ship")]
        [SerializeField] private float m_mass;

        /// <summary>
        /// Driving forward force
        /// </summary>
        [SerializeField] private float m_thrust;

        /// <summary>
        /// Rotation force
        /// </summary>
        [SerializeField] private float m_mobility;

        /// <summary>
        /// Maximal linear velocity, grad/sec
        /// </summary>
        [SerializeField] private float m_maxLinearVelocity;
        public float Speed => m_maxLinearVelocity;

        /// <summary>
        /// Maximal angular velocity
        /// </summary>
        [SerializeField] private float m_maxAngularVelocity;
        public float Agility => m_maxAngularVelocity;

        [SerializeField] private Sprite m_previewImage;
        public Sprite PreviewImage => m_previewImage;

        [SerializeField] private GameObject m_explosionPrefab;
        [SerializeField] private GameObject m_debuffAnimation;

        [SerializeField] private Turret[] m_turrets;

        [SerializeField] private int m_maxEnergy;
        [SerializeField] private int m_maxAmmo;
        [SerializeField] private int m_energyRegenPerSecond;

        /*[Header("Bars")]
        [SerializeField] private SpriteFillBar m_invincibilityBar;
        [SerializeField] private SpriteFillBar m_speedBoostBar;*/

        /*private float m_primaryEnergy;
        public float PrimaryEnergy => m_primaryEnergy;

        private int m_secondaryAmmo;
        public int SecondaryAmmo => m_secondaryAmmo;*/

        /// <summary>
        /// Saved reference to rigid
        /// </summary>
        private Rigidbody2D m_rigid;

        private float originalSpeed;

        /*private bool shieldInEffect = false;
        private bool speedBoostInEffect = false;

        private float shieldDurationTime;
        private float shieldTimer;
        private float speedBoostDurationTime;
        private float speedBoostTimer;
        private float originalThrust;*/

        #region PublicAPI

        /// <summary>
        /// Thrust (driving forward force) Control: -1.0 to 1.0
        /// </summary>
        public float ThrustControl { get; set; }

        /// <summary>
        /// Torque (rotation force) Control: -1.0 to 1.0
        /// </summary>
        public float TorqueControl { get; set; }

        public Vector2 MovingSpeed => m_rigid.velocity;

        #endregion

        #region UnityEvents

        protected override void Start()
        {
            base.Start();

            m_rigid = GetComponent<Rigidbody2D>();
            m_rigid.mass = m_mass;

            m_rigid.inertia = 1;

            //originalSpeed = m_maxLinearVelocity;
            //originalThrust = m_thrust;

            //InitOffensive();

            //if (Player.Instance.ActiveShip == this) InitBars();
        }

        private void FixedUpdate()
        {
            UpdateRigidBody();

            //UpdateEnergyRegen();

            //if (Player.Instance.ActiveShip == this) UpdatePowerups();
        }

        #endregion

        /// <summary>
        /// Method for applying forces to ship movement
        /// </summary>
        private void UpdateRigidBody()
        {
            m_rigid.AddForce(ThrustControl * m_thrust * transform.up * Time.fixedDeltaTime, ForceMode2D.Force);

            m_rigid.AddForce(-m_rigid.velocity * (m_thrust/m_maxLinearVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

            m_rigid.AddTorque(TorqueControl * m_mobility * Time.fixedDeltaTime, ForceMode2D.Force);

            m_rigid.AddTorque(-m_rigid.angularVelocity * (m_mobility / m_maxAngularVelocity) * Time.fixedDeltaTime, ForceMode2D.Force);

        }

        protected override void OnDeath()
        {
            if (m_explosionPrefab != null)
            {
                var explosion = Instantiate(m_explosionPrefab, transform.position, Quaternion.identity);

                Destroy(explosion, 1);
            }

            if (TryGetComponent(out TowerDefense.Enemy enemy))
            {
                enemy.SpawnPopups();
            }

            base.OnDeath();
        }

        // TODO method

        public bool DrawEnergy(int count)
        {
            return true;
        }

        // TODO method

        public bool DrawAmmo(int count)
        {
            return true;
        }

        // TODO method

        public void Fire(TurretMode mode)
        {
            return;
        }

        public void Use(EnemyAsset asset)
        {
            SetTeam(asset.TeamdId);
            SetHitPoints(asset.HitPoints);
            SetScoreValue(asset.ScoreValue);
            m_maxLinearVelocity = asset.MoveSpeed;
            originalSpeed = m_maxLinearVelocity;
            SetWeaknesses(asset);
        }

        public void HalfSpeed()
        {
            originalSpeed = m_maxLinearVelocity;
            m_maxLinearVelocity /= 2;

            m_debuffAnimation.SetActive(true);
        }

        public void RestoreSpeed()
        {
            m_maxLinearVelocity = originalSpeed;

            m_debuffAnimation.SetActive(false);
        }

        /*
        public void Fire(TurretMode mode)
        {
            for(int i = 0; i< m_turrets.Length; i++)
            {
                if (m_turrets[i].Mode == mode)
                {
                    m_turrets[i].Fire();
                }
            }
        }

        public void AddEnergy(int energy)
        {
            m_primaryEnergy = Mathf.Clamp(m_primaryEnergy + energy, 0, m_maxEnergy);
        }

        public void AddAmmo(int ammo)
        {
            m_secondaryAmmo = Mathf.Clamp(m_secondaryAmmo + ammo, 0, m_maxAmmo);
        }

        private void InitOffensive()
        {
            m_primaryEnergy = m_maxEnergy;
            m_secondaryAmmo = m_maxAmmo;
        }

        private void UpdateEnergyRegen()
        {
            m_primaryEnergy += (float)m_energyRegenPerSecond * Time.fixedDeltaTime;
            m_primaryEnergy = Mathf.Clamp(m_primaryEnergy, 0, m_maxEnergy);
        }

        public bool DrawEnergy(int count)
        {
            if (count == 0) return true;

            if (m_primaryEnergy >= count)
            {
                m_primaryEnergy -= count;
                return true;
            }

            return false;
        }

        public bool DrawAmmo(int count)
        {
            if (count == 0) return true;

            if (m_secondaryAmmo >= count)
            {
                m_secondaryAmmo -= count;
                return true;
            }

            return false;
        }

        public void AssignWeapon(TurretProperties props)
        {
            for(int i = 0; i < m_turrets.Length; i++)
            {
                m_turrets[i].AssignLoadout(props);
            }    
        }

        public void UpdatePowerups()
        {
            if (shieldInEffect)
            {
                shieldTimer += Time.fixedDeltaTime;

                if (m_invincibilityBar != null)
                {
                    m_invincibilityBar.gameObject.SetActive(true);
                    m_invincibilityBar.EmptyBar();
                }

                if (shieldTimer >= shieldDurationTime)
                {
                    shieldInEffect = false;

                    m_indestrutible = false;

                    if (m_invincibilityBar != null)
                    {
                        m_invincibilityBar.ResetBar();
                        m_invincibilityBar.gameObject.SetActive(false);
                    }
                }
            }

            if (speedBoostInEffect)
            {
                speedBoostTimer += Time.fixedDeltaTime;

                if (m_speedBoostBar != null)
                {
                    m_speedBoostBar.gameObject.SetActive(true);
                    m_speedBoostBar.EmptyBar();
                }

                if (speedBoostTimer >= speedBoostDurationTime)
                {
                    speedBoostInEffect = false;

                    ResetSpeed();

                    if (m_speedBoostBar != null)
                    {
                        m_speedBoostBar.ResetBar();
                        m_speedBoostBar.gameObject.SetActive(false);
                    }
                }
            }
        }

        public void GetShield(float time)
        {
            m_indestrutible = true;

            shieldDurationTime = time;
            shieldTimer = 0;

            m_invincibilityBar.ResetBar();
            m_invincibilityBar.SetFillAmountStep(shieldDurationTime);

            shieldInEffect = true;
        }

        public void GetSpeedBoost(float time, float modifier)
        {
            ResetSpeed();

            m_maxLinearVelocity *= modifier;
            m_thrust *= modifier;

            speedBoostDurationTime = time;
            speedBoostTimer = 0;

            m_speedBoostBar.ResetBar();
            m_speedBoostBar.SetFillAmountStep(speedBoostDurationTime);

            speedBoostInEffect = true;
        }

        private void ResetSpeed()
        {
            m_thrust = originalThrust;
            m_maxLinearVelocity = originalLinearVelocity;
        }

        public void InitBars()
        {
            if (m_invincibilityBar != null)
            {
                m_invincibilityBar.gameObject.SetActive(false);
            }

            if (m_speedBoostBar != null)
            {
                m_speedBoostBar.gameObject.SetActive(false);
            }
        }*/
    }
}
