using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace TowerDefense
{ 
    public class ClickProtection : SingletonBase<ClickProtection>, IPointerClickHandler
    {
        private Image blocker;
        private Action<Vector2> m_onClickAction;
        private void Start()
        {
            blocker = GetComponent<Image>();
            blocker.enabled = false;
        }

        public void Activate(Action<Vector2> mouseAction)
        {
            blocker.enabled = true;
            m_onClickAction = mouseAction;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            blocker.enabled = false;
            m_onClickAction(eventData.pressPosition);
            m_onClickAction = null;
        }
    }
}
