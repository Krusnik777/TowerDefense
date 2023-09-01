using UnityEngine;
using UnityEngine.Events;
using SpaceShooter;

namespace TowerDefense
{
    public class TDPatrolController : AIController
    {
        [SerializeField] private UnityEvent m_eventOnEndPath;
        public UnityEvent EventOnEndPath => m_eventOnEndPath;

        private Path m_path;
        private int pathIndex;

        public void SetPath(Path path)
        {
            m_path = path;
            pathIndex = 0;
            SetPatrolBehaviour(m_path[pathIndex]);
        }

        protected override void GetNewPoint()
        {
            pathIndex++;
            if (m_path.Length > pathIndex)
            {
                SetPatrolBehaviour(m_path[pathIndex]);
            }
            else
            {
                m_eventOnEndPath?.Invoke();
                Destroy(gameObject);
            }
            
        }
    }
}
