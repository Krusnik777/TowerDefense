using UnityEngine;
using TowerDefense;

namespace SpaceShooter
{
    public class Projectile : Entity
    {
        public enum DamageType
        {
            Physic,
            Magic,
            Almighty
        }

        [SerializeField] protected float m_velocity;
        [SerializeField] protected float m_lifeTime;
        [SerializeField] protected int m_damage;
        [SerializeField] protected DamageType m_damageType;
        [SerializeField] protected ImpactEffect m_impactEffectPrefab;
        [SerializeField] protected UpgradeAsset m_almightyDamageUpgrade;
        [SerializeField] private float m_upgradeModifier = 5.0f;

        protected float timer;

        protected Destructible m_parent;

        private float stepAmp = 0.001f;

        private bool byPlayer
        {
            get
            {
                if (Player.Instance != null && m_parent == Player.Instance.ActiveShip) return true;
                else return false;
            } 
        }
        public bool ByPlayer => byPlayer;

        public DamageType TypeOfDamage => m_damageType;

        private void Awake()
        {
            if (Upgrades.Instance)
            {
                if (m_damageType == DamageType.Almighty)
                {
                    var level = Upgrades.GetUpgradeLevel(m_almightyDamageUpgrade);

                    m_damage += (int)(m_upgradeModifier * level);
                }
            }
        }

        protected virtual void Update()
        {
            float stepLength = Time.deltaTime * m_velocity;
            Vector2 step = transform.up * stepLength;

            if (m_parent != null)
            {
                if (m_parent.gameObject.TryGetComponent(out SpaceShip parentShip))
                {
                    step += parentShip.MovingSpeed * stepAmp;
                }
            }
                

            RaycastHit2D hit = Physics2D.Raycast(transform.position, transform.up, stepLength);

            if (hit)
            {
                OnHit(hit);
                OnProjectileLifeEnd(hit.collider, hit.point);
            }

            timer += Time.deltaTime;

            if (timer > m_lifeTime) Destroy(gameObject);

            transform.position += new Vector3(step.x, step.y, 0);
        }

        /*
        private void OnHit(RaycastHit2D hit)
        {
            Destructible dest = hit.collider.transform.root.GetComponent<Destructible>();

            if (dest != null && dest != m_parent)
            {
                dest.ApplyDamage(this, m_damage);

                if (byPlayer)
                {
                    Player.Instance.AddScore(dest.ScoreValue);
                }
            }
            OnProjectileLifeEnd(hit.collider, hit.point);
        }*/

        private void OnHit(RaycastHit2D hit)
        {
            var enemy = hit.collider.transform.root.GetComponent<Enemy>();

            if (enemy != null)
            {
                enemy.TakeDamage(this, m_damage);
            }
            
        }

            protected void OnProjectileLifeEnd(Collider2D col, Vector2 pos)
        {
            if (m_impactEffectPrefab != null)
            {
                var impactEffectObject = Instantiate(m_impactEffectPrefab.gameObject, pos, Quaternion.identity);
                var impactEffect = impactEffectObject.GetComponent<ImpactEffect>();
                impactEffect.GetFirstCollider(col, pos);
                impactEffect.SetByPlayer(byPlayer);
            }

            Destroy(gameObject);
        }

        public void SetParentShooter (Destructible parent)
        {
            m_parent = parent;
        }
    }
}
