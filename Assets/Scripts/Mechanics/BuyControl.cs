using UnityEngine;
using System.Collections.Generic;

namespace TowerDefense
{
    public class BuyControl : MonoBehaviour
    {
        [SerializeField] private TowerBuyControl m_towerBuyPrefab;
        private List<TowerBuyControl> activeControls;

        private RectTransform rectTransform;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            BuildSite.OnClickEvent += MoveToBuildSite;
            gameObject.SetActive(false);
            
        }

        private void MoveToBuildSite(BuildSite buildSite)
        {
            if (buildSite)
            {
                var position = Camera.main.WorldToScreenPoint(buildSite.transform.root.position);
                rectTransform.anchoredPosition = position;
                activeControls = new List<TowerBuyControl>();
                foreach (var asset in buildSite.BuildableTowers)
                {
                    if (asset.IsAvailable)
                    {
                        var newControl = Instantiate(m_towerBuyPrefab, transform);
                        activeControls.Add(newControl);
                        newControl.SetTowerAsset(asset);
                    }
                }
                if (activeControls.Count > 0)
                {
                    gameObject.SetActive(true);
                    var angle = 360 / activeControls.Count;
                    for (int i = 0; i < activeControls.Count; i++)
                    {
                        var offset = Quaternion.AngleAxis(angle * i, Vector3.forward) * (Vector3.left * 140);
                        activeControls[i].transform.position += offset;
                    }

                    foreach (var tbc in GetComponentsInChildren<TowerBuyControl>())
                    {
                        tbc.SetBuildSite(buildSite.transform.root);
                    }
                } 
            }
            else
            {
                if (activeControls != null && activeControls.Count > 0)
                {
                    foreach (var control in activeControls)
                    {
                        Destroy(control.gameObject);
                    }
                    activeControls.Clear();
                }
                gameObject.SetActive(false);
            }
            
        }

        private void OnDestroy()
        {
            BuildSite.OnClickEvent -= MoveToBuildSite;
        }
    }
}
