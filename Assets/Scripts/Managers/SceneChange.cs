using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class SceneChange : MonoBehaviour
    {
        [SerializeField] private Button m_returnButton;

        private AudioSource audioSource;

        private void Awake()
        {
            m_returnButton.onClick.AddListener(ReturnToMainMenu);

            audioSource = FindObjectOfType<AudioSource>();
        }

        private void Update()
        {
            if (audioSource && !audioSource.isPlaying)
            {
                ReturnToMainMenu();
            }
        }

        public void ReturnToMainMenu()
        {
            m_returnButton.onClick.RemoveListener(ReturnToMainMenu);
            SceneManager.LoadScene(0);
        }
    }
}
