using UnityEngine;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
        }

        private void MoveToBuildSite(Transform buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.position);
                rectTransform.anchoredPosition = position;
                gameObject.SetActive(true);
            }
            else
            {
                gameObject.SetActive(false);
            }
            foreach(var tbc in GetComponentsInChildren<TowerBuyControl>())
            {
                tbc.SetBuildSite(buildSite);
            }
        }

        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }
    }
}
