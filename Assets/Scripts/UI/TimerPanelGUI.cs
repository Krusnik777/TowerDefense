using UnityEngine;
using UnityEngine.UI;
using SpaceShooter;
using System.Collections;

namespace TowerDefense
{
    public class TimerPanelGUI : MonoBehaviour
    {
        [SerializeField] private GameObject m_timerPanel;
        [SerializeField] private Image m_fillImage;
        [SerializeField] private NextWaveGUI m_nextWaveGUI;
        [SerializeField] private Text m_counterText;

        //private float fillStep;

        //private bool inProgress;

        private int duration;

        private int remainingDuration;

        private void Awake()
        {
            m_fillImage.fillAmount = 1;

            //inProgress = false;

            m_timerPanel.SetActive(false);
        }

        public void StartTimerPanel(LevelConditionTime timeCondition)
        {
            m_timerPanel.SetActive(true);
            if (m_nextWaveGUI) m_nextWaveGUI.TurnOffButton();

            duration = (int) timeCondition.LimitTime;
            remainingDuration = duration;

            StartCoroutine(UpdateTimer());


            //fillStep = 1.0f / timeCondition.LimitTime;
            //inProgress = true;
        }

        private IEnumerator UpdateTimer()
        {
            while (remainingDuration >= 0)
            {
                m_counterText.text = $"{remainingDuration / 60:00}:{remainingDuration % 60:00}";
                m_fillImage.fillAmount = Mathf.InverseLerp(0, duration, remainingDuration);
                remainingDuration--;
                yield return new WaitForSeconds(1.0f);
            }
        }    

        /*private void Update()
        {
            if (inProgress && m_fillImage.fillAmount > 0)
            {
                m_fillImage.fillAmount -= fillStep * Time.deltaTime;
            }
        }*/

    }
}
