using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    public class Abilities : SingletonBase<Abilities>
    {
        public interface Usable
        {
            void SetUpgrade();
            void SetAvailability(int crystals);
            void Use();
        }

        [Serializable]
        public class FireAbility : Usable
        {
            [SerializeField] private UpgradeAsset m_fireAbilityUpgrade;
            [SerializeField] private float m_upgradeModifier = 7.0f;
            [SerializeField] private Text m_text;
            [SerializeField] private Button m_button;
            [SerializeField] private Image m_targetingCircle;
            [SerializeField] private int m_cost = 5;
            [SerializeField] private int m_damage = 20;
            [SerializeField] private Color m_targetingColor;
            [SerializeField] private float m_radius = 5;

            private bool isAvailable;

            public void SetUpgrade()
            {
                if (Upgrades.Instance)
                {
                    var level = Upgrades.GetUpgradeLevel(m_fireAbilityUpgrade);

                    m_damage += (int)(m_upgradeModifier * level);

                    if (level <= 0)
                    {
                        isAvailable = false;
                        m_button.interactable = false;
                        m_text.text = "N/A";
                    }
                    else
                    {
                        isAvailable = true;
                        m_text.text = m_cost.ToString();
                    }
                }
            }

            public void SetAvailability(int crystals)
            {
                if (!isAvailable) return;

                if (crystals >= m_cost != m_button.interactable)
                {
                    m_button.interactable = !m_button.interactable;
                    m_text.color = m_button.interactable ? Color.white : Color.red;
                }
            }

            public void Use()
            {
                TDPlayer.Instance.ChangeCrystalAmount(-m_cost);

                ClickProtection.Instance.Activate((Vector2 v) =>
                {
                    Vector3 position = v;
                    position.z = -Camera.main.transform.position.z;
                    position = Camera.main.ScreenToWorldPoint(position);
                    foreach (var collider in Physics2D.OverlapCircleAll(position, m_radius))
                    {
                        if (collider.transform.parent.TryGetComponent(out Enemy enemy))
                        {
                            enemy.TakeDamage(this, m_damage);
                        }
                    }
                });
            }
        }

        [Serializable]
        public class SlowAbility : Usable
        {
            [SerializeField] private UpgradeAsset m_slowAbilityUpgrade;
            [SerializeField] private float m_upgradeModifier = 5.0f;
            [SerializeField] private Text m_text;
            [SerializeField] private Button m_button;
            [SerializeField] private int m_cost = 10;
            [SerializeField] private float m_cooldown = 15.0f;
            [SerializeField] private float m_duration = 5.0f;

            private bool isAvailable;
            private bool inCooldownState;

            private int discoveredCrystals;

            public void SetUpgrade()
            {
                if (Upgrades.Instance)
                {
                    var level = Upgrades.GetUpgradeLevel(m_slowAbilityUpgrade);

                    m_duration += m_upgradeModifier * level;

                    if (level <= 0)
                    {
                        isAvailable = false;
                        m_button.interactable = false;
                        m_text.text = "N/A";
                    }
                    else
                    {
                        isAvailable = true;
                        m_text.text = m_cost.ToString();
                    }
                }
            }

            public void SetAvailability(int crystals)
            {
                if (!isAvailable) return;

                if (!inCooldownState && crystals >= m_cost != m_button.interactable)
                {
                    m_button.interactable = !m_button.interactable;
                    m_text.color = m_button.interactable ? Color.white : Color.red;
                }
                discoveredCrystals = crystals;
            }

            public void Use()
            {
                TDPlayer.Instance.ChangeCrystalAmount(-m_cost);

                void Slow(Enemy enemy)
                {
                    enemy.GetComponent<SpaceShip>().HalfSpeed();
                }

                foreach(var ship in FindObjectsOfType<SpaceShip>())
                {
                    ship.HalfSpeed();
                }

                EnemyWaveManager.EventOnEnemySpawn += Slow;

                IEnumerator Restore()
                {
                    yield return new WaitForSeconds(m_duration);

                    foreach (var ship in FindObjectsOfType<SpaceShip>())
                    {
                        ship.RestoreSpeed();
                    }

                    EnemyWaveManager.EventOnEnemySpawn -= Slow;
                }

                Instance.StartCoroutine(Restore());

                IEnumerator SlowAbilityAvailability()
                {
                    inCooldownState = true;
                    m_button.interactable = false;

                    yield return new WaitForSeconds(m_cooldown);

                    inCooldownState = false;
                    SetAvailability(discoveredCrystals);
                }

                Instance.StartCoroutine(SlowAbilityAvailability());
            }
        }

        [Header("FireAbility")]
        [SerializeField] private FireAbility m_fireAbility;

        [Header("SlowAbility")]
        [SerializeField] private SlowAbility m_slowAbility;

        public void UseFireAbility() => m_fireAbility.Use();
        public void UseSlowAbility() => m_slowAbility.Use();

        private void Start()
        {
            TDPlayer.CrystalsUpdateSubscribe(CrystalAmountCheck);

            m_fireAbility.SetUpgrade();
            m_slowAbility.SetUpgrade();
        }

        private void OnDestroy()
        {
            TDPlayer.CrystalsUpdateUnsubscribe(CrystalAmountCheck);
        }

        private void CrystalAmountCheck(int crystals)
        {
            m_fireAbility.SetAvailability(crystals);
            m_slowAbility.SetAvailability(crystals);
        }

        
    }
}
