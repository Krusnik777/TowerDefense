using UnityEngine;

namespace TowerDefense
{
    public class FollowCursor : MonoBehaviour
    {
        [SerializeField] private Canvas m_parentCanvas;

        private void OnEnable()
        {
            Vector2 pos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                m_parentCanvas.transform as RectTransform, Input.mousePosition,
                m_parentCanvas.worldCamera,
                out pos);
        }

        public void Update()
        {
            Vector2 movePos;

            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                m_parentCanvas.transform as RectTransform,
                Input.mousePosition, m_parentCanvas.worldCamera,
                out movePos);

            transform.position = m_parentCanvas.transform.TransformPoint(movePos);
        }

    }
}
