using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionLimitTime : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private float m_limitTime;
        public float LimitTime => m_limitTime;

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (LevelController.Instance.LevelTime <= m_limitTime)
                    return true;
                else
                {
                    Player.Instance.ImmediateDeath();
                    return false;
                }
            }
        }
    }
}
