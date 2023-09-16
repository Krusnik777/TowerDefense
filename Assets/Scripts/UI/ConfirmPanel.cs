using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class ConfirmPanel : MonoBehaviour
    {
        [SerializeField] private Button m_confirmButton;
        [SerializeField] private Button m_declineButton;

        private MapLevel m_parent;

        private void Awake()
        {
            m_confirmButton.onClick.AddListener(OnConfirm);
            m_declineButton.onClick.AddListener(OnDecline);
        }

        private void OnDestroy()
        {
            m_confirmButton.onClick.RemoveListener(OnConfirm);
            m_declineButton.onClick.RemoveListener(OnDecline);
        }

        public void OnConfirm()
        {
            m_parent.LoadLevel();
        }

        public void OnDecline()
        {
            Destroy(gameObject);
        }

        public void SetParent(MapLevel parent)
        {
            m_parent = parent;
        }
    }
}
