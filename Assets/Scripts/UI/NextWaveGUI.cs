using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class NextWaveGUI : MonoBehaviour
    {
        [SerializeField] private Text m_bonusAmount;
        [SerializeField] private Image m_fillImage;

        private EnemyWaveManager manager;

        private Button button;

        private float timeToNextWave;

        private float fillStep;

        private void Awake()
        {
            manager = FindObjectOfType<EnemyWaveManager>();

            button = GetComponentInChildren<Button>();
            button.onClick.AddListener(CallWave);

            EnemyWave.EventOnWavePrepare += (float time) =>
            {
                timeToNextWave = time;

                if (m_fillImage) m_fillImage.fillAmount = 0;
                fillStep = 1.0f / timeToNextWave;
            };

            manager.EventOnFinalWave += () =>
            {
                button.interactable = false;
                if (m_fillImage) m_fillImage.fillAmount = 1;
                m_bonusAmount.transform.parent.gameObject.SetActive(false);
            };
        }

        private void OnDestroy()
        {
            button.onClick.RemoveListener(CallWave);
        }

        private void Update()
        {
            UpdateNextWaveGUI();
        }

        private void UpdateNextWaveGUI()
        {
            if (!m_bonusAmount.transform.parent.gameObject.activeSelf) return;

            var bonus = (int)timeToNextWave;
            if (bonus < 0) bonus = 0;
            m_bonusAmount.text = bonus.ToString();
            timeToNextWave -= Time.deltaTime;
            if (m_fillImage && m_fillImage.fillAmount < 1) m_fillImage.fillAmount += fillStep * Time.deltaTime;
        }

        public void CallWave()
        {
            manager.ForceNextWave();
        }

        public void TurnOffButton()
        {
            button.interactable = false;
        }
    }
}
