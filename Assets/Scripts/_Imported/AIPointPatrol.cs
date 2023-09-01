using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class AIPointPatrol : MonoBehaviour
    {
        [SerializeField] private float m_radius;
        public float Radius => m_radius;

        private static List<AIPointPatrol> m_allPatrolPoints;

        public static IReadOnlyCollection<AIPointPatrol> AllPatrolPoints => m_allPatrolPoints;

        protected virtual void OnEnable()
        {
            if (m_allPatrolPoints == null)
                m_allPatrolPoints = new List<AIPointPatrol>();

            m_allPatrolPoints.Add(this);
        }

        protected virtual void OnDestroy()
        {
            m_allPatrolPoints.Remove(this);
        }
        
        #if UNITY_EDITOR

        private static readonly Color gizmoColor = new Color(1, 0, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = gizmoColor;
            Gizmos.DrawSphere(transform.position, m_radius);
        }

        #endif
    }
}
