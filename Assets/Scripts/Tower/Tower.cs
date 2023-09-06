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

        private Turret[] m_turrets;
        private Destructible m_target = null;

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
                Vector2 targetVector = m_target.transform.position - transform.position;

                if (targetVector.magnitude < m_radius)
                {
                    foreach (var turret in m_turrets)
                    {
                        turret.transform.up = targetVector;
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
                    m_target = enter.transform.root.GetComponent<Destructible>();
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
