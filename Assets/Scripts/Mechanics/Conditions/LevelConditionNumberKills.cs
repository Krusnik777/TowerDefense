using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionNumberKills : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private int m_numKills;
        [SerializeField] private bool m_targetSpecificTeam;
        [SerializeField] private int m_targetedTeamId;
        [SerializeField] private bool m_onlyByPlayer;

        public int NumKills => m_numKills;
        public bool TargetSpecificTeam => m_targetSpecificTeam;
        public int TargetedTeamId => m_targetedTeamId;
        public bool OnlyByPlayer => m_onlyByPlayer;

        private int killedTargets = 0;
        public int KilledTargets => killedTargets;
        private bool reachedKills;

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (Player.Instance != null && Player.Instance.ActiveShip != null)
                {
                    if (!m_targetSpecificTeam)
                    {
                        if (Player.Instance.NumKills >= m_numKills)
                        {
                            reachedKills = true;
                        }
                    }
                    else
                    {
                        if (killedTargets >= m_numKills)
                        {
                            reachedKills = true;
                        }
                    }
                    
                }
                return reachedKills;
            }
        }

        public void AddTargetKills()
        {
            killedTargets++;
        }

    }
}
