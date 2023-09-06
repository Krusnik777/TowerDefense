using System;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class TDPlayer : Player
    {
        [SerializeField] private int m_gold = 0;
        [SerializeField] private int m_crystalAmount = 0;
        [SerializeField] private UpgradeAsset m_healthUpgrade;
        [SerializeField] private int m_upgradeModifier = 5;

        private static event Action<int> EventOnGoldUpdate;
        private static event Action<int> EventOnCrystalsUpdate;
        public static event Action<int> EventOnLifeUpdate;

        private new void Awake()
        {
            base.Awake();
            if (Upgrades.Instance)
            {
                var level = Upgrades.GetUpgradeLevel(m_healthUpgrade);
                TakeDamage(-level * m_upgradeModifier);
            }
        }

        public static void GoldUpdateSubscribe(Action<int> act)
        {
            EventOnGoldUpdate += act;
            act(Instance.m_gold);
        }

        public static void LifeUpdateSubscribe(Action<int> act)
        {
            EventOnLifeUpdate += act;
            act(Instance.Lives);
        }

        public static void CrystalsUpdateSubscribe(Action<int> act)
        {
            EventOnCrystalsUpdate += act;
            act(Instance.m_crystalAmount);
        }

        public static void GoldUpdateUnsubscribe(Action<int> act)
        {
            EventOnGoldUpdate -= act;
        }

        public static void LifeUpdateUnsubscribe(Action<int> act)
        {
            EventOnLifeUpdate -= act;
        }

        public static void CrystalsUpdateUnsubscribe(Action<int> act)
        {
            EventOnCrystalsUpdate -= act;
        }

        public static new TDPlayer Instance { get { return Player.Instance as TDPlayer; } }

        public void ChangeGold(int change)
        {
            m_gold += change;
            EventOnGoldUpdate(m_gold);
        }

        public void ReduceLife(int change)
        {
            TakeDamage(change);
            EventOnLifeUpdate(Lives);
        }

        public void ChangeCrystalAmount(int change)
        {
            m_crystalAmount += change;
            EventOnCrystalsUpdate(m_crystalAmount);
        }

        public void TryBuild(TowerAsset towerAsset, Transform buildSite)
        {
            ChangeGold(-towerAsset.GoldCost);

            var tower = Instantiate(towerAsset.Prefab, buildSite.position, Quaternion.identity);
            tower.Use(towerAsset);

            Destroy(buildSite.gameObject);
        }
    }
}
