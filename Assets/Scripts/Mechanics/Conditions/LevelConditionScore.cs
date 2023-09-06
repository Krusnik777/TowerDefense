using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionScore : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int m_score;

        public int TargetScore => m_score;

        private bool reachedScore;

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if (Player.Instance.Score >= m_score)
                    {
                        reachedScore = true;
                    }
                }
                return reachedScore;
            }
        }
    }
}
