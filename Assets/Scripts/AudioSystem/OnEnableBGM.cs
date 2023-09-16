using UnityEngine;

namespace TowerDefense
{
    public class OnEnableBGM : MonoBehaviour
    {
        [SerializeField] private Sound m_sound;
        [SerializeField] private bool m_loop;

        private void OnEnable()
        {
            m_sound.PlayBGM(m_loop);
        }
    }
}
