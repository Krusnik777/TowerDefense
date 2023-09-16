using UnityEngine;

namespace TowerDefense
{
    [RequireComponent(typeof(AudioSource))]
    public class SoundPlayer : SingletonBase<SoundPlayer>
    {
        [SerializeField] private Sounds m_sounds;

        private AudioSource m_audioSource;

        private new void Awake()
        {
            base.Awake();

            m_audioSource = GetComponent<AudioSource>();

            Instance.m_audioSource.loop = true;
            Instance.m_audioSource.clip = m_sounds[Sound.MainMenuBGM];
            Instance.m_audioSource.Play();
        }

        public void Play(Sound sound)
        {
            m_audioSource.PlayOneShot(m_sounds[sound]);
        }

        public void PlayBGM(Sound sound, bool loop)
        {
            m_audioSource.loop = loop;
            m_audioSource.clip = m_sounds[sound];
            m_audioSource.Play();
        }
    }
}
