using UnityEngine;

namespace SpaceShooter
{
    public class SpriteFillBar : MonoBehaviour
    {
        [SerializeField] private Transform m_fillImage;
        [SerializeField] [Range(0.0f,1.0f)] private float m_startScale;

        private float fillAmountStep;

        private void Start()
        {
            var newScale = m_fillImage.localScale;
            newScale.x = m_startScale;
            m_fillImage.localScale = newScale;
        }

        public void SetFillAmountStep(float fillAmount)
        {
            fillAmountStep = 1.0f/fillAmount;
        }

        public void FillBar()
        {
            if (m_fillImage.localScale.x >= 1.0f) return;

            var newScale = m_fillImage.localScale;
            newScale.x += fillAmountStep*Time.deltaTime;
            m_fillImage.localScale = newScale;
        }

        public void FillBar(float value)
        {
            if (m_fillImage.localScale.x >= 1.0f) return;

            var newScale = m_fillImage.localScale;
            newScale.x += value * Time.deltaTime;
            m_fillImage.localScale = newScale;
        }

        public void EmptyBar()
        {
            if (m_fillImage.localScale.x <= 0.0f) return;

            var newScale = m_fillImage.localScale;
            newScale.x -= fillAmountStep * Time.deltaTime;
            m_fillImage.localScale = newScale;
        }

        public void EmptyBar(float value)
        {
            if (m_fillImage.localScale.x <= 0.0f) return;

            var newScale = m_fillImage.localScale;
            newScale.x -= value*fillAmountStep;
            m_fillImage.localScale = newScale;
        }

        public void ResetBar()
        {
            var newScale = m_fillImage.localScale;
            newScale.x = m_startScale;
            m_fillImage.localScale = newScale;
        }
    }
}
