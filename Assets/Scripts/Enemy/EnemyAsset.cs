using UnityEngine;

namespace TowerDefense
{
    [CreateAssetMenu]
    public sealed class EnemyAsset : ScriptableObject
    {
        [Header("Visual")]
        public Color Color = Color.white;
        public Vector2 SpriteScale = new Vector2(3, 3);
        public RuntimeAnimatorController Animation;

        [Header("Balance")]
        public int TeamdId = 0;
        public float MoveSpeed = 1;
        public int HitPoints = 100;
        public int ScoreValue = 100;
        public float Radius = 0.19f;
        public int Damage = 1;
        public int Gold = 1;

        [Header("Elemental Weaknesses")]
        [Range(0,2)] public float PhysicWeakness;
        [Range(0,2)] public float MagicWeakness;
    }
}
