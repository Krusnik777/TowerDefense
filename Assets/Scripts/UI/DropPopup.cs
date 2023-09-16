using UnityEngine;

namespace TowerDefense
{
    public class DropPopup : MonoBehaviour
    {
        [SerializeField] private TextMesh m_goldCount;
        [SerializeField] private TextMesh m_crystalCount;

        public void SetupPopup(int droppedGold, int droppedCrystals)
        {
            m_goldCount.text = droppedGold.ToString();
            m_crystalCount.text = droppedCrystals.ToString();
        }

    }
}
