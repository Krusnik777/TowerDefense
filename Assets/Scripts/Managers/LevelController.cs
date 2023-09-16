using UnityEngine;
using UnityEngine.Events;
using TowerDefense;

namespace SpaceShooter
{
    public interface ILevelCondition
    {
        bool IsCompleted { get; }
    }

    public class LevelController : SingletonBase<LevelController>
    {
        [SerializeField] protected float m_referenceTime;
        public float ReferenceTime => m_referenceTime;

        [SerializeField] private int m_bonusScore;
        public int BonusScore => m_bonusScore;

        [SerializeField] protected UnityEvent m_eventLevelCompleted;

        private ILevelCondition[] m_conditions;

        protected bool m_isLevelCompleted;
        private float m_levelTime;
        public float LevelTime => m_levelTime;

        protected void Start()
        {
            m_conditions = GetComponentsInChildren<ILevelCondition>();
        }

        private void Update()
        {
            if (!m_isLevelCompleted)
            {
                m_levelTime += Time.deltaTime;

                CheckLevelConditions();
            }
        }

        private void CheckLevelConditions()
        {
            if (m_conditions == null || m_conditions.Length == 0) return;

            int numCompleted = 0;

            foreach (var v in m_conditions)
            {
                if (v.IsCompleted)
                    numCompleted++;
            }

            if (numCompleted == m_conditions.Length)
            {
                m_isLevelCompleted = true;
                m_eventLevelCompleted?.Invoke();

                LevelSequenceController.Instance?.FinishCurrentLevel(true);
            }
        }

        public bool TryGetActiveLevelConditionTime(out LevelConditionTime levelConditionTime)
        {
            foreach (var v in m_conditions)
            {
                if (v is LevelConditionTime)
                {
                    levelConditionTime = (LevelConditionTime)v;

                    if (levelConditionTime.gameObject.activeInHierarchy)
                    {
                        return true;
                    }
                    else return false;   
                }
            }

            levelConditionTime = null;
            return false;
        }

    }
}
