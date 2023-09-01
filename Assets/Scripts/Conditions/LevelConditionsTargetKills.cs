using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelConditionsTargetKills : MonoBehaviour, ILevelCondition
    {
        [SerializeField] private List<Destructible> m_targets;
        public List<Destructible> Targets => m_targets;

        private int numTargets;
        public int NumTargets => numTargets;

        private void Start()
        {
            numTargets = m_targets.Count;
        }

        bool ILevelCondition.IsCompleted
        {
            get
            {
                if (numTargets <= 0)
                    return true;
                else return false;
            }
        }

        public void RemoveTarget()
        {
            numTargets--;
        }
    }
}
