using UnityEngine;
using SpaceShooter;

namespace TowerDefense
{
    public class Path : MonoBehaviour
    {
        [SerializeField] private CircleArea m_startArea;
        public CircleArea StartArea => m_startArea;

        [SerializeField] private AIPointPatrol[] m_pathPoints;
        public AIPointPatrol[] PathPoints => m_pathPoints;

        public int Length { get => m_pathPoints.Length; }
        public AIPointPatrol this[int i] { get => m_pathPoints[i]; }

        #if UNITY_EDITOR

        private static readonly Color gizmoColor = new Color(0, 0.5f, 0, 0.3f);

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = gizmoColor;
            foreach (var point in m_pathPoints)
            {
                Gizmos.DrawSphere(point.transform.position, point.Radius);
            }  
        }

        #endif
    }
}
