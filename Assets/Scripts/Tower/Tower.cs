using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class Tower : MonoBehaviour
    {
        [SerializeField] private float m_radius;
        public float Radius => m_radius;
        [SerializeField] private UpgradeAsset m_towerRadiusUpgrade;
        [SerializeField] private float m_upgradeModifier = 1.0f;

        //private float m_leadPrediction = 0.25f;
        private Turret[] m_turrets;
        private Rigidbody2D m_target = null;

        private void Awake()
        {
            if (Upgrades.Instance)
            {
                var level = Upgrades.GetUpgradeLevel(m_towerRadiusUpgrade);
                m_radius += m_upgradeModifier*level;
            }
        }

        private void Start()
        {
            m_turrets = GetComponentsInChildren<Turret>();
        }

        private void Update()
        {
            if (m_target)
            {
                if (Vector3.Distance(m_target.transform.position, transform.position) <= m_radius)
                {
                    foreach (var turret in m_turrets)
                    {
                        turret.transform.up = m_target.transform.position - turret.transform.position;
                        //turret.transform.up = m_target.transform.position - turret.transform.position + (Vector3) m_target.velocity * m_leadPrediction;
                        turret.Fire();
                    }
                }
                else
                {
                    m_target = null;
                }
            }
            else
            {
                var enter = Physics2D.OverlapCircle(transform.position, m_radius);
                if (enter)
                {
                    m_target = enter.transform.root.GetComponent<Rigidbody2D>();
                }
            }
        }

        public void Use(TowerAsset asset)
        {
            var sr = GetComponentInChildren<SpriteRenderer>();
            sr.sprite = asset.Sprite;
            sr.color = asset.Color;

            var turrets = GetComponentsInChildren<Turret>();
            foreach (var turret in turrets)
            {
                turret.AssignLoadout(asset.TurretProps);
            }

            var buildSite = GetComponentInChildren<BuildSite>();
            buildSite.SetBuildableTowers(asset.m_upgradesTo);
        }


#if UNITY_EDITOR

        private static readonly Color gizmoColor = new Color(0, 1, 1, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, m_radius);
        }

        #endif
    }
}
