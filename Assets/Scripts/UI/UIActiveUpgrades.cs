using UnityEngine;
using UnityEngine.UI;

namespace TowerDefense
{
    public class UIActiveUpgrades : MonoBehaviour
    {
        [SerializeField] private Text m_text;

        private void Start()
        {
            m_text.text = "";

            if (Upgrades.Instance)
            {
                var upgrades = Upgrades.GetActiveUpgrades();

                if (upgrades.Count > 0)
                {
                    foreach (var u in upgrades)
                    {
                        m_text.text += $"{u.Asset.Name} Lv.: {((u.Level >= u.Asset.CostByLevel.Length) ? "Max" : u.Level.ToString())}\n";
                    }
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

    }
}
