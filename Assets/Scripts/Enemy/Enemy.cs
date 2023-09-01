using UnityEngine;
using SpaceShooter;
using System;

#if UNITY_EDITOR

using UnityEditor;

#endif

namespace TowerDefense
{
    [RequireComponent(typeof(TDPatrolController))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int m_damage = 1;
        [SerializeField] private int m_droppedGold = 1;

        public event Action EventOnEnd;

        private TDPatrolController AIController;
        private Destructible destructible;

        private void Start()
        {
            AIController = GetComponent<TDPatrolController>();
            AIController.EventOnEndPath.AddListener(DamagePlayer);

            destructible = GetComponent<Destructible>();
            destructible.EventOnDeath.AddListener(GiveGoldToPlayer);
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

            m_droppedGold = asset.Gold;
        }

        public void DamagePlayer()
        {
            TDPlayer.Instance.ReduceLife(m_damage);
        }

        public void GiveGoldToPlayer()
        {
            TDPlayer.Instance.ChangeGold(m_droppedGold);
        }

        private void OnDestroy()
        {
            AIController.EventOnEndPath.RemoveListener(DamagePlayer);
            destructible.EventOnDeath.RemoveListener(GiveGoldToPlayer);

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
