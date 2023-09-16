using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class LevelConditionTime : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private float m_finishTime;
        public float LimitTime => m_finishTime;

        private void Start()
        {
            var timerGUI = FindObjectOfType<TimerPanelGUI>();

            if (timerGUI) timerGUI.StartTimerPanel(this);

            m_finishTime += Time.time;
        }

        bool ILevelCondition.IsCompleted
        {
            get
            {
                return Time.time > m_finishTime;
            } 
        }
    }
}
