using UnityEngine;
using SpaceShooter;
using System;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace TowerDefense
{
    [RequireComponent(typeof(TDPatrolController),typeof(Destructible))]
    public class Enemy : MonoBehaviour
    {
        public enum ArmorType
        {
            Physic,
            Magic
        }
        public static Func<int, Projectile.DamageType, int, int>[] ArmorDamageFunctions =
        {
            (int power, Projectile.DamageType type, int armor) =>
            {
                // ArmorType.Base
                switch(type)
                {
                    case Projectile.DamageType.Magic : return power;
                    default : return Mathf.Max(power-armor, 1);
                        
                }
            },
            (int power, Projectile.DamageType type, int armor) =>
            {
                // ArmorType.Magic
                if(Projectile.DamageType.Physic == type) armor = armor/2;
                return Mathf.Max(power-armor, 1);
            }
        };

        [SerializeField] private int m_damage = 1;
        [SerializeField] private int m_armor = 1;
        [SerializeField] private ArmorType m_armorType;
        [SerializeField] private int m_droppedGold = 1;
        [SerializeField] private int m_droppedCrystals = 1;

        public event Action EventOnEnd;

        private TDPatrolController AIController;
        private Destructible destructible;

        private void Start()
        {
            AIController = GetComponent<TDPatrolController>();
            AIController.EventOnEndPath.AddListener(DamagePlayer);

            destructible = GetComponent<Destructible>();
            destructible.EventOnDeath.AddListener(GiveGoldToPlayer);
            destructible.EventOnDeath.AddListener(GiveCrystalsToPlayer);
        }

        public void Use(EnemyAsset asset)
        {
            var sr = transform.Find("Visual").GetComponent<SpriteRenderer>();
            sr.color = asset.Color;
            sr.transform.localScale = new Vector3(asset.SpriteScale.x, asset.SpriteScale.y, 1);

            sr.GetComponent<Animator>().runtimeAnimatorController = asset.Animation;

            GetComponent<SpaceShip>().Use(asset);

            GetComponentInChildren<CircleCollider2D>().radius = asset.Radius;

            m_damage = asset.Damage;
            m_armor = asset.ArmorPoints;
            m_droppedGold = asset.Gold;
            m_droppedCrystals = asset.Crystals;
        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_damage);
        }

        public void GiveGoldToPlayer()
        {
            TDPlayer.Instance.ChangeGold(m_droppedGold);
        }

        public void GiveCrystalsToPlayer()
        {
            TDPlayer.Instance.ChangeCrystalAmount(m_droppedCrystals);
        }

        public void TakeDamage(object sender, int damage)
        {
            destructible.ApplyDamage(sender, Mathf.Max(1, damage - m_armor));
        }

        public void TakeDamage(object sender, int damage, Projectile.DamageType damageType)
        {
            destructible.ApplyDamage(sender, ArmorDamageFunctions[(int)m_armorType](damage, damageType, m_armor));
        }


        private void OnDestroy()
        {
            AIController.EventOnEndPath.RemoveListener(DamagePlayer);
            destructible.EventOnDeath.RemoveListener(GiveGoldToPlayer);
            destructible.EventOnDeath.RemoveListener(GiveCrystalsToPlayer);

            EventOnEnd?.Invoke();
        }
    }

    #if UNITY_EDITOR

    [CustomEditor(typeof(Enemy))]
    public class EnemyInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EnemyAsset a = EditorGUILayout.ObjectField(null, typeof(EnemyAsset), false) as EnemyAsset;
            if (a)
            {
                (target as Enemy).Use(a);
            }
        }
    }

    #endif
}
