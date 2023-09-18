using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace SpaceShooter
{
    [CreateAssetMenu]
    public class Episode : ScriptableObject
    {
        [SerializeField] private string m_episodeName;
        public string EpisodeName => m_episodeName;

        [SerializeField] private string[] m_levels;
        public string[] Levels => m_levels;

        [SerializeField] private bool m_hasFinalScene;
        public bool HasFinalScene => m_hasFinalScene;

        [SerializeField] private AudioClip[] m_levelsBGM;
        public AudioClip[] LevelsBGM => m_levelsBGM;

        [SerializeField] private Sprite m_previewImage;
        public Sprite PreviewImage => m_previewImage;

        public string BriefingText = "Briefing Text Here";
    }

    #if UNITY_EDITOR
    [CustomEditor(typeof(Episode))]
    public class EpisodeTextInspector : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            var t = (Episode)target;
            t.BriefingText = GUILayout.TextArea(t.BriefingText, 1000);
        }
    }
    #endif
}
