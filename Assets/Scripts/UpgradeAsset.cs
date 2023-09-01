using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class UpgradeAsset : ScriptableObject
    {
        public Sprite Sprite;
        public int[] CostByLevel = { 3 };

    }
}
