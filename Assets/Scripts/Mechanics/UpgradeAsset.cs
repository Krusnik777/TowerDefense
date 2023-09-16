using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        public enum UpgradeType
        {
            Passive,
            Active
        }

        public string Name;
        public UpgradeType Type;
        public Sprite Sprite;
        public int[] CostByLevel = { 3 };
    }
}
