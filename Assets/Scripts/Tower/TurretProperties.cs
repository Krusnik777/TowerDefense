using UnityEngine;

namespace SpaceShooter
{
    public enum TurretMode
    {
        Primary,
        Secondary,
        Auto
    }

    [CreateAssetMenu]
    public sealed class TurretProperties : ScriptableObject
    {
        [SerializeField] private TurretMode m_mode;
        public TurretMode Mode => m_mode;

        [SerializeField] private Projectile m_projectilePrefab;
        public Projectile ProjectilePrefab => m_projectilePrefab;

        [SerializeField] private float m_rateOfFire;
        public float RateOfFire => m_rateOfFire;

        [SerializeField] private int m_energyUsage;
        public int EnergyUsage => m_energyUsage;

        [SerializeField] private int m_ammoUsage;
        public int AmmoUsage => m_ammoUsage;

        [SerializeField] private AudioClip m_launchSFX;
        public AudioClip LaunchSFX => m_launchSFX;
    }
}
