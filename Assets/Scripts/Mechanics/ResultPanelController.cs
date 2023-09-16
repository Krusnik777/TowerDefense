using UnityEngine;
using UnityEngine.UI;
using TowerDefense;

namespace SpaceShooter
{
    public class ResultPanelController : SingletonBase<ResultPanelController>
    {
        [SerializeField] private GameObject m_panelSuccess;
        [SerializeField] private GameObject m_panelFailure;
        [SerializeField] private Button m_returnButton;
        [SerializeField] private Button m_restartButton;

        [SerializeField] private GameObject m_panelScore;
        [SerializeField] private Image[] m_resultImages;

        //[SerializeField] private Text m_kills;
        //[SerializeField] private Text m_score;
        //[SerializeField] private Text m_time;

        //[SerializeField] private Text m_result;

        private void Start()
        {
            //gameObject.SetActive(false);
            m_panelSuccess.GetComponentInChildren<Button>().onClick.AddListener(OnButtonAdvanceLevel);
            m_returnButton.onClick.AddListener(OnButtonReturn);
            m_restartButton.onClick.AddListener(OnButtonRestartLevel);

            m_panelSuccess.SetActive(false);
            m_panelFailure.SetActive(false);
            m_panelScore.SetActive(false);
        }

        private void OnDestroy()
        {
            m_panelSuccess.GetComponentInChildren<Button>().onClick.RemoveListener(OnButtonAdvanceLevel);
            m_returnButton.onClick.RemoveListener(OnButtonReturn);
            m_restartButton.onClick.RemoveListener(OnButtonRestartLevel);
        }

        public void Show(bool result)
        {
            //gameObject.SetActive(true);

            m_panelSuccess.SetActive(result);
            m_panelFailure.SetActive(!result);

            m_panelScore?.SetActive(true);

            for (int i = 0; i < TDLevelController.Instance.LevelScore; i++)
            {
                m_resultImages[i].enabled = false;
            }

            /*m_result.text = m_success ? "Win" : "Lose";

            m_kills.text = "Kills: " + levelResults.NumKills.ToString();
            m_score.text = "Score: " + levelResults.Score.ToString();
            m_time.text = "Time: " + levelResults.Time.ToString();*/
        }

        public void OnButtonRestartLevel()
        {
            if (!m_panelSuccess.activeInHierarchy || !m_panelFailure.activeInHierarchy)
                TDLevelController.Instance.StopLevelActivity();

            LevelSequenceController.Instance.RestartLevel();
        }

        public void OnButtonAdvanceLevel()
        {
            LevelSequenceController.Instance.AdvanceLevel();
        }

        public void OnButtonReturn()
        {
            if (!m_panelSuccess.activeInHierarchy || !m_panelFailure.activeInHierarchy)
                TDLevelController.Instance.StopLevelActivity();

            LevelSequenceController.Instance.ReturnToLevelMap();
        }

        public void OnButtonExit()
        {
            if (!m_panelSuccess.activeInHierarchy || !m_panelFailure.activeInHierarchy)
                TDLevelController.Instance.StopLevelActivity();

            Application.Quit();
        }
        
    }
}
