using UnityEngine;

namespace TowerDefense
{
    public class LevelDisplayController : MonoBehaviour
    {
        [SerializeField] private MapLevel[] m_levels;
        [SerializeField] private BranchLevel[] m_branchLevels;

        private void Start()
        {
            int drawLevel = 0;

            var score = 1;

            while (score != 0 && drawLevel < m_levels.Length)
            {
                m_levels[drawLevel].Initialize(ref score);
                drawLevel++;
            }

            for(int i = drawLevel; i< m_levels.Length; i++)
            {
                m_levels[i].gameObject.SetActive(false);
            }

            for (int i = 0; i < m_branchLevels.Length; i++)
            {
                m_branchLevels[i].TryActivate();
            }
        }
    }
}
