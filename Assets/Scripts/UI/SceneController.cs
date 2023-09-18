using UnityEngine;
using UnityEngine.SceneManagement;

namespace TowerDefense
{
    public class SceneController : SingletonBase<SceneController>
    {
        public static string MainMenuSceneNickname = "MainMenu";
        public static string LevelMapSceneNickname = "LevelMap";

        private Animator animator;

        private string levelName;

        private void Start()
        {
            animator = GetComponent<Animator>();

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            animator.SetTrigger("FadeIn");
        }

        public void OnFadeInComplete()
        {
            Debug.Log("Yep");
        }

        public void OnFadeOutComplete()
        {
            if (levelName != null)
            {
                SceneManager.LoadScene(levelName);
            }
            else
            {
                Application.Quit();
            }
        }

        public void LoadSceneWithFade(string sceneName)
        {
            levelName = sceneName;
            animator.SetTrigger("FadeOut");
        }

        public void LoadMainMenu()
        {
            LoadSceneWithFade(MainMenuSceneNickname);
        }

        public void LoadLevelMap()
        {
            LoadSceneWithFade(LevelMapSceneNickname);
        }

        public void QuitGame()
        {
            levelName = null;
            animator.SetTrigger("FadeOut");
        }
    }
}
