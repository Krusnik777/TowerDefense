using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    [RequireComponent(typeof(MapLevel))]
    public class BranchLevel : MonoBehaviour
    {
        [SerializeField] private MapLevel m_rootLevel;
        [SerializeField] private Text m_needPointsText;
        [SerializeField] private int needPoints = 3;

        public void TryActivate()
        {
            gameObject.SetActive(m_rootLevel.IsComplete);
            if (needPoints > MapCompletion.Instance.TotalScore)
            {
                m_needPointsText.text = needPoints.ToString();
            }
            else
            {
                m_needPointsText.transform.parent.gameObject.SetActive(false);
                GetComponent<MapLevel>().Initialize();
            }
            
        }
    }
}
