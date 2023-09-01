using System;
using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class Upgrades : SingletonBase<Upgrades>
    {
        public const string Filename = "Upgrades.dat";

        [Serializable]
        private class UpgradeSave
        {
            public UpgradeAsset Asset;
            public int Level = 0;
        }

        [SerializeField] UpgradeSave[] m_saves;

        private new void Awake()
        {
            base.Awake();
            Saver<UpgradeSave[]>.TryLoad(Filename, ref m_saves);
        }

        public static void BuyUpgrade(UpgradeAsset asset)
        {
            foreach(var upgrade in Instance.m_saves)
            {
                if (upgrade.Asset == asset)
                {
                    upgrade.Level++;
                    Saver<UpgradeSave[]>.Save(Filename, Instance.m_saves);
                }
            }
        }

        public static int GetTotalCost()
        {
            int result = 0;

            foreach (var upgrade in Instance.m_saves)
            {
                for (int i = 0; i < upgrade.Level; i++)
                {
                    result += upgrade.Asset.CostByLevel[i];
                }
            }

            return result;
        }

        public static int GetUpgradeLevel(UpgradeAsset asset)
        {
            foreach (var upgrade in Instance.m_saves)
            {
                if (upgrade.Asset == asset)
                {
                    return upgrade.Level;
                }
            }
            return 0;
        }
    }
}
