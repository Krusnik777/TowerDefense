using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionPosition : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private ReachPoint m_reachPoint;

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (m_reachPoint != null)
                    return m_reachPoint.ReachedPointPosition;
                else return false;
            }
        }
    }
}
