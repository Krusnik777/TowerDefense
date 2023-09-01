using UnityEngine;

namespace SpaceShooter
{
    [CreateAssetMenu]
    public class Episode : ScriptableObject
    {
        [SerializeField] private string m_episodeName;
        public string EpisodeName => m_episodeName;

        [SerializeField] private string[] m_levels;
        public string[] Levels => m_levels;

        [SerializeField] private AudioClip[] m_levelsBGM;
        public AudioClip[] LevelsBGM => m_levelsBGM;

        [SerializeField] private Sprite m_previewImage;
        public Sprite PreviewImage => m_previewImage;
    }
}
