using UnityEngine;

namespace TowerDefense
{
    public class OnEnableSound : MonoBehaviour
    {
        [SerializeField] private Sound m_sound;

        private void OnEnable()
        {
            m_sound.Play();
        }
    }
}
