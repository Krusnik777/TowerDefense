using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;

namespace TowerDefense
{
    public class TimerPanelGUI : MonoBehaviour
    {
        [SerializeField] private GameObject m_timerPanel;
        [SerializeField] private Image m_fillImage;
        [SerializeField] private NextWaveGUI m_nextWaveGUI;

        private float fillStep;

        private bool inProgress;

        private void Awake()
        {
            m_fillImage.fillAmount = 1;

            inProgress = false;

            m_timerPanel.SetActive(false);
        }

        private void Update()
        {
            if (inProgress && m_fillImage.fillAmount > 0)
            {
                m_fillImage.fillAmount -= fillStep * Time.deltaTime;
            }
        }

        public void StartTimerPanel(LevelConditionTime timeCondition)
        {
            m_timerPanel.SetActive(true);
            if (m_nextWaveGUI) m_nextWaveGUI.TurnOffButton();
            fillStep = 1.0f / timeCondition.LimitTime;
            inProgress = true;
        }

    }
}
