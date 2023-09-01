using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    [CreateAssetMenu]
    public class TowerAsset : ScriptableObject
    {
        public int GoldCost = 15;
        public Sprite Sprite;
        public Color Color = Color.white;
        public Sprite GUISprite;
        public TurretProperties TurretProps;
        public Tower Prefab;
    }
}
